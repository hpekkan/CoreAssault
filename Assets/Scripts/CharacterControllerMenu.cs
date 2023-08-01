
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterControllerMenu : MonoBehaviour
{
    public GameObject[] characters;
    public int currentCharacter = 0;
    RobotFreeAnim robotAnimator;
    RobotAnimator robotAnimator2;
    public PlayerState playerState;
    Dictionary<string, string> prefs;

    private void Awake()
    {
        robotAnimator = characters[2].GetComponent<RobotFreeAnim>();
        robotAnimator2 = characters[0].GetComponent<RobotAnimator>();
        

    }
    

    public void NextCharacter()
    {

        if (characters[2].activeSelf && currentCharacter == 2)
        {
            robotAnimator.CloseRobot();
        }
        characters[currentCharacter].SetActive(false);
        currentCharacter++;
        if (currentCharacter >= characters.Length)
        {
            currentCharacter = 0;
        }
        characters[currentCharacter].SetActive(true);

        if (characters[0].activeSelf && currentCharacter == 0)
        {
            robotAnimator2.Selected();
        }
        else
        {
            robotAnimator2.Deselected();
        }

        if (characters[2].activeSelf && currentCharacter == 2)
        {
            robotAnimator.OpenRobot();
        }
    }   

    public void PreviousCharacter()
    {
        if (characters[2].activeSelf && currentCharacter == 2)
        {
            robotAnimator.CloseRobot();
        }
        characters[currentCharacter].SetActive(false);
        currentCharacter--;
        if (currentCharacter < 0)
        {
            currentCharacter = characters.Length - 1;
        }
        characters[currentCharacter].SetActive(true);
        if (characters[2].activeSelf && currentCharacter == 2)
        {
            robotAnimator.OpenRobot();
        }
        
            
        
    }

    


   
}
