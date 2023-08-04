using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    PlayerState playerState;
    Dictionary<string,string> prefs;
    public TextMeshProUGUI text;
    private int gun;
    private int character;
    private int lrf;
    public Transform guns;

    void Start()
    {
        prefs = PlayerState.LoadGame();
        text.text = prefs["username"];
        gun = int.Parse(prefs["gun"]);
        character = int.Parse(prefs["character"]);
        lrf = int.Parse(prefs["lrf"]);
        guns = GameObject.Find("laserOrigin").transform;
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
    }
}
