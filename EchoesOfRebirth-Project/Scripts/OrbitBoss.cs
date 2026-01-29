using UnityEngine;

public class OrbitBoss : MonoBehaviour
{
    public Transform boss;

    [Header("Orbit Settings")]
    public float orbitRadius = 3f;
    public float orbitSpeed = 2f;
    public float heightOffset = -1.3f;

    [HideInInspector] public int index; // Which weakpoint is this? (0, 1, 2...)
    [HideInInspector] public int totalWeakPoints; // How many are there in total?

    void Update()
    {
        if (boss == null) return;

        // 1. Calculate the spacing (e.g., if 3 wps, they are 120 degrees apart)
        float spacing = (Mathf.PI * 2f) / totalWeakPoints;

        // 2. Calculate the current angle based on time + its specific index
        float currentAngle = (Time.time * orbitSpeed) + (index * spacing);

        // 3. Set the position
        Vector3 offset = new Vector3(
            Mathf.Cos(currentAngle) * orbitRadius,
            heightOffset, 
            Mathf.Sin(currentAngle) * orbitRadius
        );

        transform.position = boss.position + offset;
        
        // Optional: Make the weakpoint face away from the boss
        transform.LookAt(transform.position + offset);
    }
}