using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public RayCastGun rayCastGun;
    public PlayerController playerController;
   
    Dictionary<string, string> prefs;


    private void Awake()
    {
        prefs = PlayerState.LoadGame();

        

    }


    public void SaveGame(string username, int character, int gun, int lrf)
    {

        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetInt("character", character);
        PlayerPrefs.SetInt("gun", gun);
        PlayerPrefs.SetInt("lrf", lrf);


        PlayerPrefs.Save();
    }

    public void saveGameStatus()
    {
        // Get relevant variables from the RayCastGun script
        int totalShots = rayCastGun.totalShots;
        int hitShots = rayCastGun.hitShots;
        int totalDamage = rayCastGun.totalDamage;
        int enemiesKilled = rayCastGun.enemiesKilled;
        float gameDuration = rayCastGun.gameDuration;
        float averageHitDistance = rayCastGun.averageHitDistance;

        PlayerPrefs.SetInt("TotalShots", totalShots);
        PlayerPrefs.SetInt("HitShots", hitShots);
        PlayerPrefs.SetInt("TotalDamage", totalDamage);
        PlayerPrefs.SetInt("EnemiesKilled", enemiesKilled);
        PlayerPrefs.SetFloat("GameDuration", gameDuration);
        PlayerPrefs.SetFloat("AverageHitDistance", averageHitDistance);

        // Save all PlayerPrefs data
        PlayerPrefs.Save();

    }


    public static Dictionary<string, string> LoadGame()
    {
        var prefs = new Dictionary<string, string>();
        string username = PlayerPrefs.GetString("username");
        int character = PlayerPrefs.GetInt("character");
        int gun = PlayerPrefs.GetInt("gun");
        int lrf = PlayerPrefs.GetInt("lrf");
        prefs.Add("username", username);
        prefs.Add("character", character.ToString());
        prefs.Add("gun", gun.ToString());
        prefs.Add("lrf", lrf.ToString());

        return prefs;
    }
}
