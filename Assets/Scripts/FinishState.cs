using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishState : MonoBehaviour
{
    Dictionary<string, string> prefs;
    // Start is called before the first frame update
    private int totalShots;
    private int hitShots ;
    private int totalDamage;
    private int enemiesKilled ;
    private float gameDuration ;
    private float averageHitDistance;
    private TextMeshProUGUI totalShotsText, hitShotsText, totalDamageText, enemiesKilledText, gameDurationText,averageHitDistanceText;

    void Start()
    {
        prefs = PlayerState.LoadGame();
        totalShots = PlayerPrefs.GetInt("TotalShots");
        hitShots = PlayerPrefs.GetInt("HitShots");
        totalDamage = PlayerPrefs.GetInt("TotalDamage");
        enemiesKilled = PlayerPrefs.GetInt("EnemiesKilled");
        gameDuration = PlayerPrefs.GetFloat("GameDuration");
        averageHitDistance = PlayerPrefs.GetFloat("AverageHitDistance");

        totalShotsText = GameObject.Find("TotalShots").GetComponent<TextMeshProUGUI>();
        hitShotsText = GameObject.Find("HitShots").GetComponent<TextMeshProUGUI>();
        totalDamageText = GameObject.Find("TotalDamage").GetComponent<TextMeshProUGUI>();
        enemiesKilledText = GameObject.Find("EnemiesKilled").GetComponent<TextMeshProUGUI>();
        gameDurationText = GameObject.Find("GameDuration").GetComponent<TextMeshProUGUI>();
        averageHitDistanceText = GameObject.Find("AverageHitDistance").GetComponent<TextMeshProUGUI>();

        totalShotsText.text = "Total Shots: "+totalShots.ToString();
        hitShotsText.text = "Hits on Target: "+hitShots.ToString();
        totalDamageText.text = "Total Damage: "+totalDamage.ToString();
        enemiesKilledText.text = "Enemies Killed: "+enemiesKilled.ToString();
        gameDurationText.text = "Played Time: "+gameDuration.ToString();
        averageHitDistanceText.text = "Average Hit Distance: "+averageHitDistance.ToString();

    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void Home()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }
    public void Exit()
    {
        Application.Quit();
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
