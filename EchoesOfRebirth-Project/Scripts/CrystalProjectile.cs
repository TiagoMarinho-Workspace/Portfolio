using UnityEngine;
using Grupo3.Player; // ADDED: To access PlayerHealth

public class CrystalProjectile : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    [Header("Homing")]
    public float homingDuration = 2f;
    public float turnSpeed = 6f;
    public float baseDelay = 1f;
    public int delayOffset = 0;

    [Header("Visuals")]
    // Adjust this if the rotation is still wrong (e.g., try 90 or -90)
    public Vector3 modelRotationOffset = new Vector3(-90f, 0f, 0f);

    [Header("Damage")]
    public int damageToPlayer = 10;

    [Header("Lifetime")]
    public float maxLifetime = 8f;

    private Transform player;
    private Rigidbody rb;
    private float homingTimer;
    private bool isHoming = true;
    private float delayTimer;
    private CharacterController playerController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerController = playerObj.GetComponent<CharacterController>();
        }

        homingTimer = homingDuration;
        delayTimer = baseDelay + delayOffset;

        Destroy(gameObject, maxLifetime);
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        // 1. Handle Delay
        if(delayTimer > 0f)
        {
            delayTimer -= Time.fixedDeltaTime;
            return;
        }

        // 2. Handle Homing Logic
        if (isHoming && player != null)
        {
            homingTimer -= Time.fixedDeltaTime;

            Vector3 targetPoint = player.position;

            if (playerController != null)
            {
                targetPoint = playerController.bounds.center;
            }

            Vector3 directionToPlayer = (targetPoint - transform.position).normalized;
            Vector3 targetVelocity = directionToPlayer * speed;

            Vector3 currentVelocity = rb.linearVelocity;
            if (currentVelocity == Vector3.zero) currentVelocity = transform.forward * speed;

            Vector3 newVelocity = Vector3.RotateTowards(
                currentVelocity,                
                targetVelocity,                  
                turnSpeed * Time.fixedDeltaTime,
                10f                               
            );

            rb.linearVelocity = newVelocity;

            if (homingTimer <= 0f)
            {
                isHoming = false;
            }
        }
        else
        {
            // Maintain speed but keep current direction
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }

        // 3. Handle Rotation (Visuals)
        // We do this OUTSIDE the homing check so the crystal faces its
        // movement direction even after it stops homing.
        if (rb.linearVelocity != Vector3.zero)
        {
            // Calculate the rotation required to look at the velocity
            Quaternion lookRotation = Quaternion.LookRotation(rb.linearVelocity);
            
            // Apply the offset (Look Rotation * Offset Rotation)
            // Order matters: Apply Look first, then Offset locally
            transform.rotation = lookRotation * Quaternion.Euler(modelRotationOffset);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // MODIFIED: Apply damage here
            other.GetComponent<PlayerHealth>()?.TakeDamage(damageToPlayer);

            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Map"))
        {
            Destroy(gameObject);
        }
    }
}