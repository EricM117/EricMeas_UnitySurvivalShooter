using UnityEngine;

public class AmmoPowerup : MonoBehaviour
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
        Weapon stats = player.GetComponentInChildren<Weapon>();
        stats.currentAmmo += stats.maxAmmo;

        // Remove power up object
        Destroy(gameObject);
    }
}
