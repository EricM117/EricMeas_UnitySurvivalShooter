using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;

    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private NavMeshAgent nav;

    void Awake()
    {
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
