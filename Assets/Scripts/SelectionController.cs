using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionController : MonoBehaviour
{
    
    PlayerState playerState;

    private void Awake()
    {
        playerState = GetComponent<PlayerState>();
    }
    public void StartGame()
    {

        playerState.SaveGame(playerState.username, playerState.characterController.currentCharacter,playerState.gunController.currentGun,playerState.LRFController.currentLRF,playerState.difficultyLevel);

        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
