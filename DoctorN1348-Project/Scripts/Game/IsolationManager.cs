using UnityEngine;

public class IsolationManager : MonoBehaviour
{
    private CharacterManager characterManager;

    private void Awake()
    {
        characterManager = FindObjectOfType<CharacterManager>();
    }

    public void IsolateCurrentPatient()
    {
        CharacterManager cm = FindObjectOfType<CharacterManager>();

        if (cm == null)
        {
            Debug.LogError("[Isolation] CharacterManager not found!");
            return;
        }

        GameObject patientObj = cm.GetActiveCharacter();

        if (patientObj == null)
        {
            Debug.LogError("[Isolation] No active patient!");
            return;
        }

        PatientController patient = patientObj.GetComponent<PatientController>();

        if (patient == null)
        {
            Debug.LogError("[Isolation] Patient missing PatientController!");
            return;
        }

        bool hasPesteNegra = patient.HasIllness("Peste Negra");

        if (hasPesteNegra)
        {
            Debug.Log("[Isolation] Patient had Peste Negra → HEAL REWARD but POPULATION --");

            // Give healing coins (same as heal)
            GameManager.Instance.AddCoins(10);

            // Still remove from population (treated as permanently isolated)
            GameManager.Instance.DecreasePopulation();
        }
        else
        {
            Debug.Log("[Isolation] Patient did NOT have Peste Negra → KILLED");

            // Kill logic: decrease population but no coin reward
            GameManager.Instance.DecreasePopulation();
        }

        // Call your existing ResetPatient() inside CharacterManager
        var resetMethod = cm.GetType().GetMethod(
            "ResetPatient",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
        );

        if (resetMethod != null)
            resetMethod.Invoke(cm, new object[] { patientObj });

        // Hide old patient
        patientObj.SetActive(false);

        // Spawn a fresh new random patient
        cm.ChooseRandomCharacter();

        Debug.Log("[Isolation] Isolation complete — patient replaced.");
    }

    public void SendAwayCurrentPatient()
    {
        PatientController patient = null;

        GameObject active = characterManager.GetActiveCharacter();
        if (active != null)
            patient = active.GetComponent<PatientController>();

        if (patient == null)
        {
            Debug.LogWarning("No patient found!");
            return;
        }

        if (patient.IsHealthy())
        {
            Debug.Log("Healthy patient sent away safely!");
        }
        else
        {
            Debug.Log("Sick patient sent away → counted as death");
            GameManager.Instance.DecreasePopulation();
        }

        patient.ResetToHealthy();
        patient.HideAllIndicators();
        patient.ClearIllnesses();

        characterManager.ChooseRandomCharacter();
    }
}
