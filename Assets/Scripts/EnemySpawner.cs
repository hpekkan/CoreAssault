using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject X, Y, Z;
    public Transform player;
    public int enemyCount;
    public float spawnTime = 3f;
    public float currentEnemyCount = 0f;
    public float maxDistance;
    public int currentX=0, currentY=0, currentZ=0;

    public int maxX, maxY, maxZ;
    public enum EnemyType
    {
        X,
        Y,
        Z
    }
    public EnemyType enemyType;
    public enum hardMode
    {
        easy,
        medium,
        hard
    }
    public hardMode hardmode;


    void Start()
    {

        if (hardmode == hardMode.easy)
        {
            enemyCount = 10;
            spawnTime = 5f;
            maxDistance = 100f;
            maxX = 10;
            maxY = 2;
            maxZ = 1;

        }
        else if (hardmode == hardMode.medium)
        {
            enemyCount = 15;
            spawnTime = 3f;
            maxDistance = 75f;
            maxX = 10;
            maxY = 10;
            maxZ = 1;
        }
        else if (hardmode == hardMode.hard)
        {
            enemyCount = 20;
            spawnTime = 1f;
            maxDistance = 50f;
            maxX = 15;
            maxY = 10;
            maxZ = 3;
        }

        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (currentEnemyCount < enemyCount)
        {

            float randomDistance = Random.Range(7f, maxDistance);

            float randomAngle = Random.Range(0f, 2f * Mathf.PI);

            Vector3 offset = new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle)) * randomDistance;
            Vector3 spawnPosition = player.position + offset;

            Quaternion spawnRotation = Quaternion.identity;
            int randomEnemy = Random.Range(0, 3);

            if (randomEnemy == 0 && currentX < maxX)
            {
                Instantiate(X, spawnPosition, spawnRotation);
                currentX += 1;
                currentEnemyCount++;
            }
            else if (randomEnemy == 1 && currentY < maxY)
            {
                Instantiate(Y, spawnPosition, spawnRotation);
                currentY += 1;
                currentEnemyCount++;
            }
            else if (randomEnemy == 2 && currentZ < maxZ)
            {
                Instantiate(Z, spawnPosition, spawnRotation);
                currentZ += 1;
                currentEnemyCount++;
            }
            else EnemySpawn();

            
            yield return new WaitForSeconds(spawnTime);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
