using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public bool isDialogueOpen = false;
    private void ToggleDialogue()
    {
        isDialogueOpen = !isDialogueOpen;
        gameObject.SetActive(isDialogueOpen);
    }
    
    public void CloseDialogue()
    {
        if (isDialogueOpen)
        {
            ToggleDialogue();
        }
    }

    public void OpenDialogue()
    {
        if (!isDialogueOpen)
        {
            ToggleDialogue();
        }
    }
}
