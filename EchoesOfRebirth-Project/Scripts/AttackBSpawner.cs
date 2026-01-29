using UnityEngine;

public class AttackBSpawner : MonoBehaviour
{
    public GameObject crystalProjectilePrefab;
    public Transform firePoint;

    public int projectileCount = 6;
    public float spreadRadius = 1.5f;

    public void Spawn()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-spreadRadius, spreadRadius),
                0f,
                Random.Range(-spreadRadius, spreadRadius)
            );

            GameObject crystal = Instantiate(
                crystalProjectilePrefab,
                firePoint.position + offset,
                Quaternion.identity
            );

            crystal.GetComponent<CrystalProjectile>().delayOffset = i;
        }
    }
}
