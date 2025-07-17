using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    private GameObject player;
    private EnemyAttack enemyAttack;
    private EnemyMovement enemyMovement;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAttack = enemy.GetComponent<EnemyAttack>();
        enemyMovement = enemy.GetComponent<EnemyMovement>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnTime, spawnTime);
    }

    // Have a list of enemies to track, have a method to re-enable the nav mesh

    void Spawn()
    {
        if (playerHealth.currentHealth <= 0)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        enemyAttack.player = player;
        enemyMovement.player = player.transform;
    }

}
