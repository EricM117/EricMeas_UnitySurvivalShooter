using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // TODO: Add in check for death of player and enemy
        if (enemyHealth.currentHealth > 0 &&  playerHealth.currentHealth > 0 && playerHealth.currentLives > 0)
        {
            nav.SetDestination(player.position);
        }

        else
        {
            nav.enabled = false;
        }
        
    }
}
