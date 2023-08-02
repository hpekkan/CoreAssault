using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType
    {
        X,
        Y,
        Z
    }

    public EnemyType enemyType;
    public float moveSpeedMin = 1f;
    public float moveSpeedMax = 10f;
    private float moveSpeed;
    public float maxHealth = 1000;
    private float currentHealth;

    public GameObject playerCharacter;
    public Transform player;
    private Rigidbody rb;

    private static List<EnemyController> allEnemies = new List<EnemyController>();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        player = playerCharacter.transform;
        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
        allEnemies.Add(this);
    }

    private void FixedUpdate()
    {
        if (player)
        {
            Vector3 moveDirection = (player.position - transform.position).normalized;
            rb.velocity = moveDirection * moveSpeed;
            rb.MoveRotation(Quaternion.LookRotation(moveDirection));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Game over when colliding with the player.
            Debug.Log("Collided with the player! Game over.");
            GameOver();
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Enemy took " + damage + " damage.");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // Remove the enemy from the list when it dies and check the game status.
            allEnemies.Remove(this);
            CheckGameStatus();
            Destroy(gameObject);
        }
    }

    private void CheckGameStatus()
    {
        // End the game if all enemies are killed.
        if (allEnemies.Count == 0)
        {
            Debug.Log("All enemies killed! Game over.");
            GameOver();
        }
    }

    private void GameOver()
    {
        // Pause the game or implement other game-ending actions.
        //Time.timeScale = 0f;
        
        SceneManager.LoadScene("FinishScene", LoadSceneMode.Single);
    }
}
