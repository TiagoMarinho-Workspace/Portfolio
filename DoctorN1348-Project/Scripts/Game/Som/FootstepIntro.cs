using System.Collections;
using UnityEngine;

public class FootstepIntro : MonoBehaviour
{
    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip[] footstepClips;
    public int stepsCount = 8;
    public float minStepDelay = 0.3f;
    public float maxStepDelay = 0.45f;

    [Header("Objeto do diálogo (ou personagem)")]
    public GameObject dialogueObject;   // DialogueController, Canvas do diálogo, etc.

    private IEnumerator Start()
    {
        // Garante que o diálogo ainda não aparece
        if (dialogueObject != null)
            dialogueObject.SetActive(false);

        // Toca uma sequência de passos
        for (int i = 0; i < stepsCount; i++)
        {
            var clip = footstepClips[Random.Range(0, footstepClips.Length)];
            audioSource.PlayOneShot(clip);

            float delay = Random.Range(minStepDelay, maxStepDelay);
            yield return new WaitForSeconds(delay);
        }

        // Depois dos passos, ativa o diálogo (ou personagem)
        if (dialogueObject != null)
            dialogueObject.SetActive(true);
    }
}
