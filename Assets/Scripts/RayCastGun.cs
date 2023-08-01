using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayCastGun : MonoBehaviour
{
    // Public variables for customization
    public Camera playerCamera;
    public Transform laserOrigin;
    public float laserDuration = 0.1f;
    public float fireRate = 0.25f;
    public float x = 0.5f, y = 0.5f, z = 0.5f;
    public Material hitMaterial;
    // Weapon Battery System
    public enum BatteryCapacity { Capacity5000, Capacity10000, Capacity15000 }
    public BatteryCapacity batteryCapacity = BatteryCapacity.Capacity5000;
    private float currentBattery;

    // Weapon Fire System
    private bool isFiring = true;
    private float loadedEnergy;

    // Weapon Shooting System
    public enum WeaponRange { Range10, Range15, Range20 }
    public WeaponRange weaponRange = WeaponRange.Range10;


    // Weapon Cooling System
    public float minTemperature = 20f;
    public float maxTemperature = 70f;
    private float currentTemperature = 20f;
    public float coolingDuration = 5f;
    float initialLaserWidth;
    LineRenderer laserLine;
    float fireTimer = 0f;

    private void Awake()
    {
        currentBattery = GetBatteryCapacityValue(batteryCapacity);
        laserLine = GetComponent<LineRenderer>();
        initialLaserWidth = 0.1f;

    }

    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireTimer > fireRate)
        {
            fireTimer = 0f;

            if (!isFiring && currentTemperature <= minTemperature)
            {
                isFiring = true;
                StartCoroutine(EnergyLoading());
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isFiring = false;
            Shoot();
        }
        coolingDuration = Mathf.Pow(2, currentTemperature / 10) / Mathf.Pow(2, currentTemperature / 20);
        if (currentTemperature > minTemperature && !isFiring)
        {
            currentTemperature = Mathf.Max(currentTemperature - (Time.deltaTime * (50f / coolingDuration)), minTemperature);
        }
    }

    IEnumerator EnergyLoading()
    {
        float elapsedTime = 0f;
        float startWidth = laserLine.startWidth;
        float targetWidth = 1f; // Increase width by a factor of 2
        float energyLoadingDuration = 5f;



        while (isFiring)
        {
            elapsedTime += Time.deltaTime;
            loadedEnergy = Mathf.Pow(2, elapsedTime) * 100;

            
            float width = Mathf.Lerp(startWidth, targetWidth, elapsedTime / energyLoadingDuration);
            laserLine.startWidth = width;
            laserLine.endWidth = width;
            if (elapsedTime >= energyLoadingDuration)
            {
                
                yield break; // Stop the coroutine after Shoot() is called
            }

            yield return null; // Pause the coroutine and resume in the next frame
        }
        Shoot();
        // The coroutine will only reach this point if !isFiring
        laserLine.startWidth = initialLaserWidth;
        laserLine.endWidth = initialLaserWidth;

        // Reset the laser width and height after energy loading is complete
        isFiring = false;
    }

    void Shoot()
    {
        float usedEnergy = loadedEnergy;
        loadedEnergy = 0f;
        // Check if there is enough battery and temperature for shooting
        if (usedEnergy <= currentBattery && currentTemperature <= minTemperature)
        {
            currentBattery -= usedEnergy;
            currentTemperature += usedEnergy / 10f;
            currentTemperature = Mathf.Min(currentTemperature, maxTemperature);
            Fire();
        }
    }

    void Fire()
    {
        float range = GetWeaponRangeValue(weaponRange);

        Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(x, y, z));
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, laserOrigin.forward, out hit, range))
        {
            float distance_to_target = Vector3.Distance(rayOrigin, hit.point);
            float firePower = CalculateFirePower(loadedEnergy, distance_to_target);
            laserLine.SetPosition(0, laserOrigin.position);
            laserLine.SetPosition(1, hit.point);
            //Destroy(hit.collider.gameObject);

            // Deal damage to the target based on the calculated fire power

            if (firePower > 0f)
            {
                DealDamage(hit.collider.gameObject, firePower);
            }


        }
        else
        {
            laserLine.SetPosition(0, laserOrigin.position);
            laserLine.SetPosition(1, rayOrigin + (laserOrigin.forward * range));
        }

        StartCoroutine(RenderLaser());
    }

    void DealDamage(GameObject target, float damage)
    {
        Renderer enemyRenderer = target.GetComponent<Renderer>();
        if (enemyRenderer != null)
        {
            Material originalMaterial = enemyRenderer.material;

            // Set the hit material to the enemy
            enemyRenderer.material = hitMaterial;
            Debug.Log(target.gameObject.name + " is hit by " + damage + " damage");

            StartCoroutine(ResetMaterialAfterDelay(enemyRenderer, originalMaterial, 0.2f));
        }


    }
    IEnumerator ResetMaterialAfterDelay(Renderer renderer, Material originalMaterial, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Reset the enemy's material back to the original
        renderer.material = originalMaterial;
    }

    float GetCoolingDuration()
    {
        float currentTemp = currentTemperature;
        return (currentTemp - minTemperature) / (maxTemperature - minTemperature) * coolingDuration / 2f;
    }

    IEnumerator RenderLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }



    private float CalculateFirePower(float loadedEnergy, float distance_to_target)
    {
        return loadedEnergy * (1 - Mathf.Log(distance_to_target) / Mathf.Log(20f));
    }







    float GetBatteryCapacityValue(BatteryCapacity capacity)
    {
        switch (capacity)
        {
            case BatteryCapacity.Capacity5000:
                return 5000f;
            case BatteryCapacity.Capacity10000:
                return 10000f;
            case BatteryCapacity.Capacity15000:
                return 15000f;
            default:
                return 5000f;
        }
    }

    // Helper method to get the range value based on the chosen enum
    float GetWeaponRangeValue(WeaponRange range)
    {
        switch (range)
        {
            case WeaponRange.Range10:
                return 10f;
            case WeaponRange.Range15:
                return 15f;
            case WeaponRange.Range20:
                return 20f;
            default:
                return 10f;
        }
    }
}
