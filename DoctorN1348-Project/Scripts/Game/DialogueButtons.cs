using UnityEngine;

public class DialogueButtons : MonoBehaviour
{
    public DialogueUI dialogueUI; // arrasta no Inspector

    public void Button1()
    {
        dialogueUI.Option1();
    }

    public void Button2()
    {
        dialogueUI.Option2();
    }

    public void Button3()
    {
        dialogueUI.Option3();
    }
}
