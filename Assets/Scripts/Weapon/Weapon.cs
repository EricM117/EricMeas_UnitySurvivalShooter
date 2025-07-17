using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damagePerShot = 20;
    public float range = 100f;
    public float timeBetweenBullets = 0.15f;
    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime = 1f;

    private bool isReloading = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReloading && currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo && currentAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }

    public bool CanShoot() 
    {  
        return currentAmmo > 0 && !isReloading; 
    }

    public void UseAmmo()
    {
        currentAmmo--;
    }

    IEnumerator Reload()
    {
        isReloading = true; // Check that the player is reloading
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false; // Then they can shoot after reload is done
        Debug.Log("Reload complete.");
    }
}
