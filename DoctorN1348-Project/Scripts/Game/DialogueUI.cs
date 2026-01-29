using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [Header("UI Reference")]
    public TMP_Text dialogueText; // arrasta o TextMeshPro aqui

    [Header("Option Dialogues")]
    [TextArea] public string[] option1Dialogues;
    [TextArea] public string[] option2Dialogues;
    [TextArea] public string[] option3Dialogues;

    // Shows random dialogue from list
    public void Option1()
    {
        if (option1Dialogues.Length == 0) return;
        int index = Random.Range(0, option1Dialogues.Length);
        dialogueText.text = option1Dialogues[index];
        dialogueText.gameObject.SetActive(true);
    }

    public void Option2()
    {
        if (option2Dialogues.Length == 0) return;
        int index = Random.Range(0, option2Dialogues.Length);
        dialogueText.text = option2Dialogues[index];
        dialogueText.gameObject.SetActive(true);
    }

    public void Option3()
    {
        if (option3Dialogues.Length == 0) return;
        int index = Random.Range(0, option3Dialogues.Length);
        dialogueText.text = option3Dialogues[index];
        dialogueText.gameObject.SetActive(true);
    }
}
