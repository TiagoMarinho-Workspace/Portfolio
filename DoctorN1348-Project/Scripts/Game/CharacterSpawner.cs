using UnityEngine;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{
    public static CharacterSpawner Instance;

    [Header("Patient Prefabs")]
    public List<GameObject> patientPrefabs; // Assign 4 character prefabs in Inspector

    [Header("Spawn Position")]
    public Transform spawnPoint; // Empty object where characters appear

    private GameObject currentPatient;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnRandomCharacter();
    }

    public void SpawnRandomCharacter()
    {
        // Destroy old patient if exists
        if (currentPatient != null)
            Destroy(currentPatient);

        if (patientPrefabs.Count == 0)
        {
            Debug.LogError("No patient prefabs assigned to CharacterSpawner!");
            return;
        }

        // Choose random prefab
        int index = Random.Range(0, patientPrefabs.Count);
        GameObject prefab = patientPrefabs[index];

        // Spawn and store it
        currentPatient = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
}
