using UnityEngine;
using System.Reflection; // Required for accessing private variables

public class WeakPoint : MonoBehaviour
{
    [Header("WeakPoint Stats")]
    public int maxHealth = 30;
    private int currentHealth;

    public bool isDestroyed;

    [HideInInspector]
    public BossController boss;

    public void Activate()
    {
        currentHealth = maxHealth;
        isDestroyed = false;
        gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        // 1. Check if the object is on the Player layer
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;

        // 2. Look for the MeleeHitbox component
        MonoBehaviour melee = other.GetComponent("MeleeHitbox") as MonoBehaviour;
        
        if (melee != null)
        {
            if (isDestroyed) return;

            // 3. REFLECTION: Reach inside and grab the private _damageAmount
            int damageToApply = 20; // Default fallback
            
            FieldInfo field = melee.GetType().GetField("_damageAmount", 
                BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null)
            {
                damageToApply = (int)field.GetValue(melee);
            }

            // 4. Apply the damage
            currentHealth -= damageToApply;

            if (boss != null)
                boss.TakeDamage(damageToApply);

            Debug.Log($"<color=white>Weakpoint Hit!</color> Pulled private damage: {damageToApply}");

            if (currentHealth <= 0)
            {
                isDestroyed = true;
                gameObject.SetActive(false);
            }
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}