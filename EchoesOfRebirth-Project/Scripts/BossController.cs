using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Grupo3.Player;
using UnityEngine.UI;
using TMPro;

public class BossController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private Transform player;

    [Header("Status")]
    public BossState currentState;
    private bool isAttacking;
    private bool isDashing;
    private int attackLayer = 0;

    [Header("Music")]
    public AudioClip bossMusic;
    [Range(0, 1)] public float musicVolume = 0.5f;
    private AudioSource musicSource;

    [Header("Rotation")]
    public float rotationSpeed = 8f;

    [Header("Circle Movement")]
    public float radius = 5f;
    public float angularSpeed = 1f;
    private float angle;

    [Header("Weak Points")]
    public List<WeakPoint> weakPoints;
    public float staggerDuration = 8f;
    private float staggerTimer;

    [Header("Health")]
    public int maxHealth = 300;
    public int currentHealth;
    public CrystalBossHealthBar healthBarScript;

    [Header("Attacks")]
    public BossAttackType[] phase1Attacks;
    public BossAttackType[] phase2Attacks;
    public BossAttackType[] phase3Attacks;

    public enum BossAttackType
    {
        AttackA,
        AttackB,
        AttackC,
        AttackD
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = maxHealth;
        SetupBossMusic();
    }

    void Update()
    {
        FacePlayer();
    }

    void FacePlayer()
    {
        if (!player || isDashing) return;

        Vector3 dir = player.position - transform.position;
        dir.y = 0f;

        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            rot,
            rotationSpeed * Time.deltaTime
        );
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthBarScript?.UpdateHealth(currentHealth, maxHealth);
    }

    void SetupBossMusic()
    {
        if (!bossMusic) return;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = bossMusic;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }
}
