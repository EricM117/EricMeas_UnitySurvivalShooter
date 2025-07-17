using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public WeaponSwitching weaponSwitching; // Get access to the weapon switching
    public LineRenderer gunLine;
    public LineRenderer gunLineLeft;
    public LineRenderer gunLineRight;
    
    private float timer;
    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootableMask;
    private ParticleSystem gunParticles;
    private AudioSource gunAudio;
    private Light gunLight;
    private float effectsDisplayTime = .2f;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;

        Weapon currentWeapon = GetCurrentWeapon();

        if (Input.GetButton("Fire1") && timer >= currentWeapon.timeBetweenBullets && currentWeapon.CanShoot() && !PauseMenu.gameIsPaused)
        {
            Shoot(currentWeapon); 
        }

        if (timer  >= currentWeapon.timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    Weapon GetCurrentWeapon() // Finds the current weapon/gameobject in the WeaponSwitching script and returns the Weapon script in that object for it to be used in this script
    {
        GameObject weaponObject = weaponSwitching.weapons[weaponSwitching.selectedWeapon]; 
        return weaponObject.GetComponent<Weapon>();
    }

    private void Shoot(Weapon weapon)
    {
        if (weaponSwitching.selectedWeapon == 0 || weaponSwitching.selectedWeapon == 1)
        {
            timer = 0f;
            weapon.UseAmmo();

            gunAudio.Play();
            gunLight.enabled = true;
            gunParticles.Stop();
            gunParticles.Play();
            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, weapon.range, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null) // If the script is there
                {
                    enemyHealth.TakeDamage(weapon.damagePerShot, shootHit.point);
                }

                gunLine.SetPosition(1, shootHit.point);
            }

            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * weapon.range);
            }
        }

        if (weaponSwitching.selectedWeapon == 2)
        {
            timer = 0f;
            weapon.UseAmmo();

            gunAudio.Play();
            gunLight.enabled = true;
            gunParticles.Stop();
            gunParticles.Play();
            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);
            gunLineLeft.enabled = true;
            gunLineLeft.SetPosition(0, transform.position);
            gunLineRight.enabled = true;
            gunLineRight.SetPosition(0, transform.position);

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, weapon.range, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null) // If the script is there
                {
                    enemyHealth.TakeDamage(weapon.damagePerShot, shootHit.point);
                }

                Debug.Log("Shotgun Hits");
                Vector3 shotgun_Left = new Vector3(shootHit.point.x, shootHit.point.y, shootHit.point.z - 3);
                Vector3 shotgun_Right = new Vector3(shootHit.point.x, shootHit.point.y, shootHit.point.z + 3);
                gunLine.SetPosition(1, shootHit.point);
                gunLineLeft.SetPosition(1, shotgun_Left);
                gunLineRight.SetPosition(1, shotgun_Right);
            }

            else
            {
                Debug.Log("Shotgun Misses");
                Vector3 shootPoint = shootRay.origin + shootRay.direction * weapon.range;
                Vector3 shotgun_Left = new Vector3(shootHit.point.x - 5, shootHit.point.y, shootHit.point.z - 3);
                Vector3 shotgun_Right = new Vector3(shootHit.point.x + 5, shootHit.point.y, shootHit.point.z + 3);
                gunLine.SetPosition(1, shootPoint);
                gunLineLeft.SetPosition(1, shotgun_Left);
                gunLineRight.SetPosition(1, shotgun_Right);
            }
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
        gunLineLeft.enabled = false;
        gunLineRight.enabled = false;
    }
}
