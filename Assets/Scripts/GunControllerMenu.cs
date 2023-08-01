
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GunControllerMenu : MonoBehaviour
{
    public GameObject[] guns;
    public int currentGun = 0;


    public void NextGun()
    {

        guns[currentGun].SetActive(false);
        currentGun = (currentGun + 1) % guns.Length;
        guns[currentGun].SetActive(true);

        

        
    }

    public void PreviousGun()
    {
        guns[currentGun].SetActive(false);
        currentGun--;
        if (currentGun < 0)
        {
            currentGun = guns.Length - 1;
        }
        guns[currentGun].SetActive(true);
        
    }




}
