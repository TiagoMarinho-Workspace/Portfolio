using UnityEngine;



public class BossSpawnTrigger : MonoBehaviour

{

    [Header("Boss Spawn")]

    public GameObject bossPrefab;

    public Transform spawnPoint;



    private bool hasSpawned = false;



    private void OnTriggerEnter(Collider other)

    {

        if (hasSpawned)

            return;



        if (other.CompareTag("Player"))

        {

            SpawnBoss();

        }

    }



    void SpawnBoss()

    {

        hasSpawned = true;



        Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);



        // Optional: disable trigger after use

        gameObject.SetActive(false);

    }

}