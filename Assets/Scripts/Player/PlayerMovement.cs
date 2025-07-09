using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 6f;
    public float dashSpeed = 25f; // Container for the dashing speed
    public float dashTimer = 0.2f; // The duration of the dash
    public float dashCooldown = 5f; // Timer between next dash
    public AudioClip dashClip; // Container for dash sound effect

    Vector3 movement;
    Animator anim;
    AudioSource playerAudio;
    Rigidbody playerRB;
    int floorMask;
    float camRayLength = 100f;
    float currentSpeed; // Container to store player speed for future speed changes
    bool canDash = true; // Check to make sure that the player can dash

    [SerializeField] private TrailRenderer tr; // Container for the trail renderer, makes it public

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerRB = GetComponent<Rigidbody>();
    }

    void Start()
    {
        tr.emitting = false;
        currentSpeed = playerSpeed;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
        Dashing();
        
    }

    void Move(float h, float v)
    {
        movement.Set (h, 0f, v);
        movement = movement.normalized * currentSpeed * Time.deltaTime;

        playerRB.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRB.MoveRotation (newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

    void Dashing()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canDash && movement != Vector3.zero)
        {
            StartCoroutine(DashingTimer());
        }
    }

    IEnumerator DashingTimer()
    {
        tr.emitting = true; // Turns on trail renderer
        currentSpeed = dashSpeed; // Changes current speed to the dashing speed
        playerAudio.clip = dashClip;
        playerAudio.Play(); // Plays "dash"
        yield return new WaitForSeconds(dashTimer); // Duration of the dash
        tr.emitting = false; // Turns off trail renderer after dash is dine
        currentSpeed = playerSpeed; // Changes player speed back to normal player speed
        canDash = false; // Player can't dash while they're on cooldown
        yield return new WaitForSeconds(dashCooldown); // Cooldown between next dash
        canDash = true; // Player able to dash again after cool down
    }
}
