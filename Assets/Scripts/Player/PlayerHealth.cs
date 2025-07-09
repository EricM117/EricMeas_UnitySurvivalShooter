using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingLives = 3;
    public int currentLives;
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public AudioClip hurtClip;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, .1f);
    public GameObject player;

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool isDamaged;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
    }

    void Start()
    {
        currentHealth = startingHealth;
        currentLives = startingLives;
    }

    void Update()
    {
        if (isDamaged)
        {
            damageImage.color = flashColor;
        }

        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        isDamaged = false;
    }

    public void TakeDamage(int amount)
    {
        isDamaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.clip = hurtClip;
        playerAudio.Play(); // Plays "hurt"

        if (currentHealth <= 0 && !isDead )
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        playerShooting.DisableEffects();
        anim.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play(); // Plays "death"
        playerMovement.enabled = false;
        playerShooting.enabled = false;
        currentLives--;

        if (currentLives > 0)
        {
            StartCoroutine(RevivePlayer());
        }
    }

    IEnumerator RevivePlayer()
    {
        yield return new WaitForSeconds(3f);
        isDead = false;
        anim.SetTrigger("Respawn");
        playerMovement.enabled = true;
        playerShooting.enabled = true;
        currentHealth = startingHealth;
    }

    public void RestartLevel()
    {
        Debug.Log("Restart Level");
    }
}
