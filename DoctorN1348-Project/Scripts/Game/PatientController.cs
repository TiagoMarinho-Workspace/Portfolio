using UnityEngine;
using System;
using System.Collections.Generic;

public class PatientController : MonoBehaviour
{
    public Sprite healthySprite;       
    public Sprite feverSprite;         
    public Sprite redBruiseSprite;     
    public Sprite purpleBruiseSprite;  
    public Sprite mucusSprite;         
    public Sprite sweatSprite;         

    // Child GameObjects for illness indicators
    public GameObject feverIndicator;
    public GameObject redBruiseIndicator;
    public GameObject purpleBruiseIndicator;
    public GameObject mucusIndicator;
    public GameObject sweatIndicator;

    private SpriteRenderer spriteRenderer;
    private Dictionary<string, List<string>> treatmentItems; // Updated to hold multiple treatments
    private HashSet<string> selectedIllnesses; // Store selected illnesses for healing check

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectedIllnesses = new HashSet<string>();   // <-- ADD THIS

        spriteRenderer.sprite = healthySprite; 
        HideAllIndicators(); 
        InitializeTreatments();
    }
    
    private void OnEnable()
    {

        // ensure renderer is present
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = healthySprite;
        HideAllIndicators();

        // reset illnesses collection and then assign new ones
        selectedIllnesses = new HashSet<string>();
        AssignRandomIllnesses();
    }




    private void InitializeTreatments()
    {
        treatmentItems = new Dictionary<string, List<string>>
        {
            { "Gripe", new List<string> { "Water", "Chamomile" } },
            { "Fadiga", new List<string> { "Bread"} },
            { "Tuberculose", new List<string> { "Honey", "Mint" } },
            { "Erisipela", new List<string> { "Syringe" } },
            { "Infeção da Pele", new List<string> { "Cream", "Vinegar" } },
            { "Peste Negra", new List<string> { "Isolation" } }
        };
    }

    public void ChangeHealthState(string state)
    {
        switch (state)
        {
            case "healthy":
                // Keep sprite the same
                break; 
            case "fever":
                feverIndicator.SetActive(true); 
                break;
            case "redBruise":
                redBruiseIndicator.SetActive(true);
                break;
            case "purpleBruise":
                purpleBruiseIndicator.SetActive(true);
                break;
            case "mucus":
                mucusIndicator.SetActive(true);
                break;
            case "sweat":
                sweatIndicator.SetActive(true); 
                break;
        }
    }

    public void ShowDetail(string detail)
    {
        switch (detail)
        {
            case "fever":
                feverIndicator.SetActive(true);
                break;
            case "redBruise":
                redBruiseIndicator.SetActive(true);
                break;
            case "purpleBruise":
                purpleBruiseIndicator.SetActive(true);
                break;
            case "mucus":
                mucusIndicator.SetActive(true);
                break;
            case "sweat":
                sweatIndicator.SetActive(true);
                break;
        }
    }

    public void HideDetail(string detail)
    {
        switch (detail)
        {
            case "fever":
                feverIndicator.SetActive(false);
                break;
            case "redBruise":
                redBruiseIndicator.SetActive(false);
                break;
            case "purpleBruise":
                purpleBruiseIndicator.SetActive(false);
                break;
            case "mucus":
                mucusIndicator.SetActive(false);
                break;
            case "sweat":
                sweatIndicator.SetActive(false);
                break;
        }
    }
    
    public void ResetToHealthy()
    {
        spriteRenderer.sprite = healthySprite;
    }

    public void ClearIllnesses()
    {
        if (selectedIllnesses != null)
            selectedIllnesses.Clear();
    }

    public void HideAllIndicators()
    {
        feverIndicator.SetActive(false);
        redBruiseIndicator.SetActive(false);
        purpleBruiseIndicator.SetActive(false);
        mucusIndicator.SetActive(false);
        sweatIndicator.SetActive(false);
    }


    public void AssignRandomIllnesses()
    {

        // RESET EVERYTHING HERE
        selectedIllnesses.Clear(); 
        HideAllIndicators();
        spriteRenderer.sprite = healthySprite;

        List<string> illnesses = new List<string>
        {
            "Gripe",
            "Fadiga",
            "Tuberculose",
            "Erisipela",
            "Infeção da Pele",
            "Peste Negra"
        };

        int numberOfIllnesses = UnityEngine.Random.Range(0, 3);

        if (numberOfIllnesses == 0)
        {
            Debug.Log("[Patient] " + gameObject.name + " chosen healthy (0 illnesses).");
            return;
        }

        while (selectedIllnesses.Count < numberOfIllnesses)
        {
            int illnessIndex = UnityEngine.Random.Range(0, illnesses.Count);
            selectedIllnesses.Add(illnesses[illnessIndex]);
        }

        Debug.Log("[Patient] " + gameObject.name + " illnesses chosen: " + string.Join(", ", selectedIllnesses));

        foreach (var illness in selectedIllnesses)
        {
            switch (illness)
            {
                case "Gripe":
                    ShowDetail("fever");
                    ShowDetail("mucus");
                    break;

                case "Fadiga":
                    break;

                case "Tuberculose":
                    ShowDetail("purpleBruise");
                    ShowDetail("sweat");
                    break;

                case "Erisipela":
                    ShowDetail("redBruise");
                    ShowDetail("fever");
                    break;

                case "Infeção da Pele":
                    ShowDetail("purpleBruise");
                    ShowDetail("redBruise");
                    break;

                case "Peste Negra":
                    break;
            }
        }
    }

    public bool HasIllness(string illnessName)
    {
        return selectedIllnesses.Contains(illnessName);
    }

    public bool IsHealthy()
    {
        return selectedIllnesses.Count == 0;
    }

    public void OnItemDropped()
    {
        // Call the AttemptTreatment() method from the TreatmentArea
        TreatmentArea treatmentArea = UnityEngine.Object.FindFirstObjectByType<TreatmentArea>();
        if (treatmentArea != null)
        {
            treatmentArea.AttemptTreatment(this);
        }
    }

    public void AttemptTreatment(List<string> treatments)
    {
        foreach (var illness in selectedIllnesses)
        {
            // Check against each treatment needed for the illnesses
            if (treatmentItems.ContainsKey(illness))
            {
                foreach (var treatment in treatments)
                {
                    if (treatmentItems[illness].Contains(treatment))
                    {
                        HealCharacter();
                        return; // Exit after healing
                    }
                }
            }
        }

        KillCharacter(); // If no treatments are effective
    }

    private void HealCharacter()
    {
        Debug.Log("Character healed successfully!");
        GameManager.Instance.AddCoins(10);

        spriteRenderer.sprite = healthySprite;
        HideAllIndicators();

        selectedIllnesses = new HashSet<string>();

        CharacterManager manager = FindObjectOfType<CharacterManager>();
        if (manager != null)
            manager.ChooseRandomCharacter();
    }

    private void KillCharacter()
    {
        Debug.Log("Character has died!");
        GameManager.Instance.DecreasePopulation();

        spriteRenderer.sprite = healthySprite;
        HideAllIndicators();

        selectedIllnesses = new HashSet<string>();

        CharacterManager manager = FindObjectOfType<CharacterManager>();
        if (manager != null)
            manager.ChooseRandomCharacter();
    }
}