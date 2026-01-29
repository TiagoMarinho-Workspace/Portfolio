using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bowl : MonoBehaviour
{
    private List<ItemData> itemsInBowl = new List<ItemData>();

    public GameObject balloonUI;
    public Transform iconsContainer;
    public GameObject iconPrefab;

    [Header("Balloon Layout")]
    public float rightShift = 0.5f;


    private void Start()
    {
        if (balloonUI != null)
            balloonUI.SetActive(false);
    }

    public void AddItem(GameObject item, ItemData data)
    {
        itemsInBowl.Add(data);
        UpdateBalloon();
    }

    private void UpdateBalloon()
    {
        if (balloonUI == null || iconsContainer == null || iconPrefab == null)
        {
            Debug.LogError("Bowl UI references missing!");
            return;
        }

        balloonUI.SetActive(true);

        // Clear previous icons
        foreach (Transform t in iconsContainer)
            Destroy(t.gameObject);

        Debug.Log("Updating balloon… items count: " + itemsInBowl.Count);
       

        float spacing = 1f;
        int count = itemsInBowl.Count;

        // Total width of the layout
        float totalWidth = (count - 1) * spacing;

        // Start centered
        float offsetX = -totalWidth / 2f;

        // Shift should shrink when more icons appear
        float shift = (count > 1) ? rightShift / count : 0f;

        foreach (ItemData item in itemsInBowl)
        {
            GameObject iconObj = new GameObject(item.itemName + "_Icon");
            iconObj.transform.SetParent(iconsContainer);

            iconObj.transform.localPosition = new Vector3(offsetX + shift, 0, 0);
            iconObj.transform.localScale = Vector3.one * 0.3f;

            SpriteRenderer sr = iconObj.AddComponent<SpriteRenderer>();
            sr.sprite = item.icon;
            sr.sortingOrder = 10;

            offsetX += spacing;
        }
    }

    public void UseItemsOnPatient(PatientController patient)
    {
        if (patient == null)
        {
            Debug.LogError("Patient is NULL when trying to use bowl items!");
            return;
        }

        // Convert ItemData list into a list of item IDs or names for patient logic
        List<string> treatments = new List<string>();

        foreach (var item in itemsInBowl)
        {
            treatments.Add(item.itemName); 
        }

        // Let the patient try to heal using these items
        patient.AttemptTreatment(treatments);

        // Clear bowl after use
        ClearBowl();
    }

    public void ClearBowl()
    {
        itemsInBowl.Clear();
        if (balloonUI != null)
            balloonUI.SetActive(false);
    }
}
