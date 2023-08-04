using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public GameObject HealthBar;
    
    Vector3 rot = new Vector3(0, 180, 0);
	float rotSpeed = 40f;
    public int health = 2;
    int currHealth;
    float step;
    //float rotSpeed = 40f;
    Animator anim;


    private int width = 406;
    private int height = 44;

    public GameObject gameController;

    // Use this for initialization
    void Awake()
    {
        gameController = GameObject.Find("GameController");
        anim = gameObject.GetComponent<Animator>();
        gameObject.transform.eulerAngles = rot;
        HealthBar = GameObject.FindWithTag("HealthBar");
        currHealth = health;
        step = 1f / health;
    }

    // Update is called once per frame
    void Update()
    {
        CheckKey();
        gameObject.transform.eulerAngles = rot;
    }


    public void OpenRobot()
    {
        anim.SetBool("Open_Anim", true);
    }
    public void CloseRobot()
    {
        anim.SetBool("Open_Anim", false);
    }

    public void RollRobot()
    {
        anim.SetBool("Roll_Anim", true);

    }

    void CheckKey()
    {
        /*// Walk
		if (Input.GetKey(KeyCode.W))
		{
			anim.SetBool("Walk_Anim", true);
		}
		else if (Input.GetKeyUp(KeyCode.W))
		{
			anim.SetBool("Walk_Anim", false);
		}
		*/
		// Rotate Left
		if (Input.GetKey(KeyCode.A))
		{
			rot[1] -= rotSpeed * Time.fixedDeltaTime;
            anim.SetBool("Walk_Anim", true);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("Walk_Anim", false);
        }

        // Rotate Right
        if (Input.GetKey(KeyCode.D))
		{
			rot[1] += rotSpeed * Time.fixedDeltaTime;
            anim.SetBool("Walk_Anim", true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("Walk_Anim", false);
        }
        /*
		// Roll
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (anim.GetBool("Roll_Anim"))
			{
				anim.SetBool("Roll_Anim", false);
			}
			else
			{
				anim.SetBool("Roll_Anim", true);
			}
		}

		// Close
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			if (!anim.GetBool("Open_Anim"))
			{
				anim.SetBool("Open_Anim", true);
			}
			else
			{
				anim.SetBool("Open_Anim", false);
			}
		}*/
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            anim.SetBool("Walk_Anim", false);
            health--;
            HealthBar.GetComponent<Slider>().value -= step;
            Destroy(collision.gameObject);
            if(health <= 0)
            {
                GameOver();
            }
        }
    }
    private void GameOver()
    {
        GameState gameState =  gameController.GetComponent<GameState>();

        gameState.saveGameStatus();

        SceneManager.LoadScene("FinishScene", LoadSceneMode.Single);
    }


}
