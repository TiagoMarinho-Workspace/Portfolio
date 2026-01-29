using UnityEngine;

public class SendAwayManager : MonoBehaviour
{
    // Chama este método pelo botão OnClick
    public void SendAwayCurrentPatient()
    {
        // Encontra o CharacterManager existente
        CharacterManager cm = FindObjectOfType<CharacterManager>();
        if (cm == null)
        {
            Debug.LogError("[SendAway] CharacterManager not found!");
            return;
        }

        // Pega no GameObject activo e obtém o PatientController
        GameObject active = cm.GetActiveCharacter();
        if (active == null)
        {
            Debug.LogWarning("[SendAway] No active patient!");
            return;
        }

        PatientController patient = active.GetComponent<PatientController>();
        if (patient == null)
        {
            Debug.LogError("[SendAway] Active GameObject has no PatientController!");
            return;
        }

        // Usa o método público IsHealthy() em vez de aceder selectedIllnesses
        if (patient.IsHealthy())
        {
            Debug.Log("[SendAway] Patient is healthy → reward given");
            GameManager.Instance.AddCoins(10); // mesmo valor que heal
        }
        else
        {
            Debug.Log("[SendAway] Patient has illness → counted as death");
            GameManager.Instance.DecreasePopulation();
        }

        // Reset visual/state do paciente (usa os métodos públicos que já tens)
        patient.ResetToHealthy();
        patient.HideAllIndicators();
        patient.ClearIllnesses();

        // Troca para próximo paciente
        cm.ChooseRandomCharacter();
    }
}
