using UnityEngine;
using System.Collections.Generic;

public class TreatmentArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item")) // Ensure items have the "Item" tag
        {
            collision.transform.SetParent(transform); // Parent the item to the TreatmentArea
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            collision.transform.SetParent(null); // Unparent the item if it exits the drop area
        }
    }

    public void AttemptTreatment(PatientController patient)
    {
        // Gather all items in this drop area and pass them to the AttemptTreatment method
        List<string> itemsToUse = new List<string>();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Item"))
            {
                itemsToUse.Add(child.name); // Use the GameObject's name (or any other identifier)
            }
        }

        patient.AttemptTreatment(itemsToUse);
    }
}