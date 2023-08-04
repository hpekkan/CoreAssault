using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public RayCastGun rayCastGun;
    public PlayerController playerController;
    public CharacterControllerMenu characterController;
    public GunControllerMenu gunController;
    public LRFControllerMenu LRFController;
    public Text inputField;
    public string username;
    Dictionary<string, string> prefs;
    public GameObject usernameField;

    public TextMeshProUGUI difficulty;
    public int difficultyLevel = 1;

    private void Awake()
    {
        prefs = PlayerState.LoadGame();
        difficulty = GameObject.Find("Difficulty").GetComponent<TextMeshProUGUI>();
        characterController.currentCharacter = int.Parse(prefs["character"]);
        gunController.currentGun = int.Parse(prefs["gun"]);
        LRFController.currentLRF = int.Parse(prefs["lrf"]);
        username = prefs["username"];
        for (int i = 0; i < characterController.characters.Length; i++)
        {
            characterController.characters[i].SetActive(false);
        }
        characterController.characters[characterController.currentCharacter].SetActive(true);

        for (int i = 0; i < gunController.guns.Length; i++)
        {
            gunController.guns[i].SetActive(false);
        }
        gunController.guns[gunController.currentGun].SetActive(true);

        for (int i = 0; i < LRFController.lrfs.Length; i++)
        {
            LRFController.lrfs[i].SetActive(false);
        }
        LRFController.lrfs[LRFController.currentLRF].SetActive(true);
        GameObject.Find("Placeholder").GetComponent<TextMeshProUGUI>().text = prefs["username"];
        usernameField = GameObject.Find("UserName");
        if(username == "")
        {
            usernameField.GetComponent<TMP_InputField>().text = "Enter Username...";
        }
        usernameField.GetComponent<TMP_InputField>().text = prefs["username"];
        if (prefs["difficulty"]!="")
        difficultyLevel = int.Parse(prefs["difficulty"]);


    }
    public void NextDifficulty()
    {
        difficultyLevel++;
        if (difficultyLevel > 3)
        {
            difficultyLevel = 1;
        }
        
    }
    public void PreviousDifficulty()
    {
        difficultyLevel--;
        if (difficultyLevel < 1)
        {
            difficultyLevel = 3;
        }
    }
    public void SaveGame(string username,int character,int gun,int lrf,int diff)
    {
      
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetInt("character", character);
        PlayerPrefs.SetInt("gun", gun);
        PlayerPrefs.SetInt("lrf", lrf);
        PlayerPrefs.SetInt("difficulty", diff);

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

        PlayerPrefs.SetInt("TotalShots", totalShots);
        PlayerPrefs.SetInt("HitShots", hitShots);
        PlayerPrefs.SetInt("TotalDamage", totalDamage);
        PlayerPrefs.SetInt("EnemiesKilled", enemiesKilled);
        PlayerPrefs.SetFloat("GameDuration", gameDuration);

        // Save all PlayerPrefs data
        PlayerPrefs.Save();

    }


    public static Dictionary<string,string> LoadGame()
    {
        var prefs = new Dictionary<string, string>();
        string username = PlayerPrefs.GetString("username");
        int character = PlayerPrefs.GetInt("character");
        int gun = PlayerPrefs.GetInt("gun");
        int lrf = PlayerPrefs.GetInt("lrf");
        int difficulty = PlayerPrefs.GetInt("difficulty");
        prefs.Add("username", username);
        prefs.Add("character", character.ToString());
        prefs.Add("gun", gun.ToString());
        prefs.Add("lrf", lrf.ToString());
        prefs.Add("difficulty", difficulty.ToString());

        return prefs;
    }
    private void Update()
    {
        username = usernameField.GetComponent<TMP_InputField>().text;
        if(username == "")
        {
            usernameField.GetComponent<TMP_InputField>().text = "Enter a name...";
            usernameField.GetComponent<TMP_InputField>().textComponent.color = Color.gray;
        }else
        {
            usernameField.GetComponent<TMP_InputField>().textComponent.color = Color.black;

        }
        if (difficultyLevel == 0) difficultyLevel = 1;
        difficulty.text =  difficultyLevel.ToString();
    }
}
