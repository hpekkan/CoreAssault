
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class LRFControllerMenu : MonoBehaviour
{
    public GameObject[] lrfs;
    public int currentLRF = 0;

    public void NextGun()
    {

        lrfs[currentLRF].SetActive(false);
        currentLRF = (currentLRF + 1) % lrfs.Length;
        lrfs[currentLRF].SetActive(true);

    }

    public void PreviousGun()
    {
        lrfs[currentLRF].SetActive(false);
        currentLRF--;
        if (currentLRF < 0)
        {
            currentLRF = lrfs.Length - 1;
        }
        lrfs[currentLRF].SetActive(true);

    }




}
