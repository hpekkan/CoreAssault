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
    void Start()
    {
        prefs = PlayerState.LoadGame();
        text.text = prefs["username"];
    }
}
