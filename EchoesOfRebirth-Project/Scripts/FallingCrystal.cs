using UnityEngine;
using System.Collections;
using Grupo3.Player; // ADDED: To access PlayerHealth

public class FallingCrystal : MonoBehaviour
{
    [Header("Settings")]
    public float heightOffset = 2f; // Distance above player's current Y
    public float telegraphDuration = 1.5f;
    public float fallSpeed = 25f;
    public int damage = 20;

    [Header("Visuals")]
    public GameObject warningAreaPrefab;
    public Color warningColor = Color.red;
    public Color dodgeColor = Color.blue;

    private GameObject warningArea;
    private Renderer warningRenderer;
    private Transform playerTransform;
    private bool isFalling = false;
    private bool isTracking = true;

    public void Setup(Transform target)
    {
        playerTransform = target;
        
        // 1. Position the crystal itself above the player
        transform.position = playerTransform.position + Vector3.up * heightOffset;
        
        // 2. Spawn the warning area
        warningArea = Instantiate(warningAreaPrefab);
        
        // 3. IMMEDIATELY set the warning area position to the player's feet
        // We use the player's X and Z, but keep Y at ground level (e.g., 0.01f)
        Vector3 groundPos = new Vector3(playerTransform.position.x, 0.01f, playerTransform.position.z);
        warningArea.transform.position = groundPos;

        warningRenderer = warningArea.GetComponent<Renderer>();
        warningRenderer.material.color = warningColor;

        StartCoroutine(AttackSequence());
    }

    void Update()
    {
        
        if (isTracking && playerTransform != null && warningArea != null)
        {
            // Follow player position
            Vector3 targetPos = playerTransform.position + Vector3.up * heightOffset;
            transform.position = targetPos;

            // Keep warning area exactly under the crystal/player
            warningArea.transform.position = new Vector3(playerTransform.position.x, 0.01f, playerTransform.position.z);
        }

        if (isFalling)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

    IEnumerator AttackSequence()
    {
        float elapsed = 0;
        while (elapsed < telegraphDuration)
        {
            elapsed += Time.deltaTime;

            // Change color to blue to signal the "Dodge Now" window
            if (elapsed / telegraphDuration > 0.75f)
            {
                warningRenderer.material.color = dodgeColor;
            }
            yield return null;
        }

        // STOP tracking so the player can actually move out of the way
        isTracking = false;
        isFalling = true;

        // Destroy warning area after a tiny delay or immediately
        Destroy(warningArea, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // MODIFIED: Damage Logic Uncommented
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null) health.TakeDamage(damage); 

            // This will now handle both the crystal and the warning area
            DestroyCrystal();
        }
        else if (other.CompareTag("Map"))
        {
            DestroyCrystal();
        }
    }

    private void DestroyCrystal()
    {
        // 1. Clean up the warning area if it still exists
        if (warningArea != null)
        {
            Destroy(warningArea);
        }

        // 2. Play sound/effects here if you have them
        // AudioSource.PlayClipAtPoint(impactSound, transform.position);

        // 3. Destroy the crystal itself
        Destroy(gameObject);
    }
}