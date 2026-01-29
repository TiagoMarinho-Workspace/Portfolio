using UnityEngine;

public class LaserWarningArea : MonoBehaviour
{
    [HideInInspector] public bool playerInside;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }
}