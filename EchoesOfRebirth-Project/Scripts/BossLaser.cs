using UnityEngine;
using UnityEngine.VFX;
using System.Collections;
using Grupo3.Player; // ADDED: To access PlayerHealth

public class BossLaser : MonoBehaviour
{
    [Header("References")]
    public VisualEffect laserVFX;
    private AudioSource laserAudio;

    [Header("Laser Settings")]
    public float duration = 2f;
    public int damagePerTick = 10; // ADDED: Damage per tick
    public float tickRate = 0.2f;  // ADDED: Frequency of damage
    private bool isFiring;
    private float nextTickTime;    // ADDED: Timer

    void Awake()
    {
        Debug.Log($"[Laser Debug] {gameObject.name} Spawned at {Time.time}");
        if (!laserVFX)
            laserVFX = GetComponentInChildren<VisualEffect>();
        
        laserAudio = GetComponent<AudioSource>();
    }

    public void Fire(Vector3 direction, float length)
    {
        if (isFiring)
        {
            Debug.LogWarning($"[Laser Debug] Fire() called on {gameObject.name}, but it's already firing!");
            return;
        }

        isFiring = true;
        Debug.Log($"[Laser Debug] {gameObject.name} starting fire sequence. Duration: {duration}");

        transform.rotation = Quaternion.LookRotation(direction);

        if (laserVFX != null)
        {
            laserVFX.Play();
        }

        if (laserAudio != null)
        {
            laserAudio.Play();
        }

        StartCoroutine(LaserLifetimeRoutine());
    }

    // MODIFIED: Continuous Damage Logic
    void Update()
    {
        if (isFiring && Time.time >= nextTickTime)
        {
            // Check for player in beam
            if (Physics.SphereCast(transform.position, 1.2f, transform.forward, out RaycastHit hit, 40f))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    hit.collider.GetComponent<PlayerHealth>()?.TakeDamage(damagePerTick);
                    nextTickTime = Time.time + tickRate;
                }
            }
        }
    }

    IEnumerator LaserLifetimeRoutine()
    {
        Debug.Log($"[Laser Debug] {gameObject.name} timer started for {duration} seconds.");
        
        yield return new WaitForSeconds(duration);

        Debug.Log($"[Laser Debug] {gameObject.name} timer finished. Starting cleanup.");

        if (laserVFX != null) laserVFX.Stop();
        if (laserAudio != null) laserAudio.Stop();

        Debug.Log($"[Laser Debug] {gameObject.name} calling Destroy(gameObject) now.");
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log($"[Laser Debug] {gameObject.name} has been successfully REMOVED from the memory.");
    }
}