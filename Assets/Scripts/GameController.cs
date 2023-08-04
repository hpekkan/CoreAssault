using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    PlayerState playerState;
    RayCastGun rayCastGun;
    LRF lrfScript;
    Dictionary<string,string> prefs;
    public TextMeshProUGUI text;
    private int gun;
    private int character;
    private int lrf;
    public Transform guns;
    public Transform characters;
    public Transform lrfs;

    void Start()
    {
        rayCastGun = GameObject.Find("laserOrigin").GetComponent<RayCastGun>();
        lrfScript = GameObject.Find("LRFs").GetComponent<LRF>();
        prefs = PlayerState.LoadGame();
        text.text = prefs["username"];
        gun = int.Parse(prefs["gun"]);
        character = int.Parse(prefs["character"]);
        lrf = int.Parse(prefs["lrf"]);
        guns = GameObject.Find("laserOrigin").transform;
        lrfs = GameObject.Find("LRFs").transform;

        for(int i =0;i< guns.childCount; i++)
        {
            if(i== gun)
            {
                guns.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                guns.GetChild(i).gameObject.SetActive(false);
            }
        }
        characters = GameObject.Find("Characters").transform;
        for (int i = 0; i < characters.childCount; i++)
        {
            if (i == character)
            {
                characters.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                characters.GetChild(i).gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < lrfs.childCount; i++)
        {
            if (i == lrf)
            {
                lrfs.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                lrfs.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (gun == 0)
        {
            rayCastGun.batteryCapacity = RayCastGun.BatteryCapacity.Capacity5000;
            rayCastGun.weaponRange = RayCastGun.WeaponRange.Range10;
        }else if(gun == 1)
        {
            rayCastGun.batteryCapacity = RayCastGun.BatteryCapacity.Capacity10000;
            rayCastGun.weaponRange = RayCastGun.WeaponRange.Range15;
        }else if(gun == 2)
        {
            rayCastGun.batteryCapacity = RayCastGun.BatteryCapacity.Capacity15000;
            rayCastGun.weaponRange = RayCastGun.WeaponRange.Range20;
        }
        if (lrf == 0)
        {
            lrfScript.angle = 30f;
            lrfScript.lrftype = LRF.lrfType.A;
        }else if(lrf == 1)
        {
            lrfScript.angle = 60f;
            lrfScript.lrftype = LRF.lrfType.B;
        }
        else if (lrf == 2)
        {
            lrfScript.angle = 90f;
            lrfScript.lrftype = LRF.lrfType.C;
        }
        Debug.Log("gun: " + gun + " character: " + character + " lrf: " + lrf);

    }
}
