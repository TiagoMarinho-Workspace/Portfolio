using UnityEngine;
using System.Collections;

public class AttackDLaserController : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject warningAreaPrefab;
    public GameObject laserPrefab;

    [Header("References")]
    public Transform laserOrigin;

    [Header("Timing")]
    public float chargeTime = 2.5f;

    [Header("Laser Settings")]
    public float laserLength = 30f;

    private GameObject activeWarning;
    private Quaternion lockedRotation;

    public void StartAttack()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        LockLaserRotation();

        if (warningAreaPrefab != null)
        {
            Vector3 groundSpawnPos = new Vector3(
                laserOrigin.position.x,
                0.1f,
                laserOrigin.position.z
            );

            activeWarning = Instantiate(
                warningAreaPrefab,
                groundSpawnPos,
                lockedRotation
            );
        }

        yield return new WaitForSeconds(chargeTime);

        if (activeWarning)
            Destroy(activeWarning);

        GameObject laserGO = Instantiate(
            laserPrefab,
            laserOrigin.position,
            lockedRotation
        );

        BossLaser laserScript = laserGO.GetComponent<BossLaser>();
        if (laserScript != null)
        {
            laserScript.Fire(laserGO.transform.forward, laserLength);
        }
    }

    void LockLaserRotation()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null) return;

        Vector3 targetPos = playerObj.transform.position;
        targetPos.y = 0.1f;

        Vector3 originFlattened = laserOrigin.position;
        originFlattened.y = 0.1f;

        Vector3 direction = (targetPos - originFlattened).normalized;
        lockedRotation = Quaternion.LookRotation(direction);
    }
}
