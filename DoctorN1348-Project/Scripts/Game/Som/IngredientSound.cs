using UnityEngine;

public class IngredientSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundClip;

    public void PlaySound()
    {
        if (audioSource != null && soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }
    }
}
