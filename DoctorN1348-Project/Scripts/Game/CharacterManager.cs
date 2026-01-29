using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] characters; // 4 patients placed in the scene
    private GameObject activeCharacter;

    private void Start()
    {
        HideAllCharacters();
        ChooseRandomCharacter();
    }

    private void HideAllCharacters()
    {
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }
    }

    public void ChooseRandomCharacter()
    {
        HideAllCharacters();

        int index = Random.Range(0, characters.Length);
        activeCharacter = characters[index];

        Debug.Log($"[CharacterManager] Choosing index {index} -> {activeCharacter.name}");

        activeCharacter.SetActive(true);
    }


    private void ResetPatient(GameObject patient)
    {
        PatientController controller = patient.GetComponent<PatientController>();

        if (controller == null)
        {
            Debug.LogError("Character missing PatientController!");
            return;
        }

        // Reset ALL patient state
        controller.HideAllIndicators();      // make private → change to public
        controller.ClearIllnesses();         // <-- we add this
        controller.AssignRandomIllnesses();  // reset illnesses

        // Reset to healthy sprite
        controller.ResetToHealthy();         // <-- we add this
    }

    public GameObject GetActiveCharacter()
    {
        return activeCharacter;
    }
}
