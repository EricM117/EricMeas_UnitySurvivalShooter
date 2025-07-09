using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    public GameObject pickupEffect;
    public ParticleSystem particleEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Spawn a cool effect
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Pickup(other);
        }
    }

    void Pickup(Collider player)
    {
        Debug.Log("Powerup picked up");

        // Apply effect to the player
        PlayerHealth stats = player.GetComponent<PlayerHealth>();
        stats.currentHealth += 50;

        // Remove power up object
        Destroy(gameObject);
    }
}
