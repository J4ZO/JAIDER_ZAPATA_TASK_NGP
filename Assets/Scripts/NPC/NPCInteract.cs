using System;
using TMPro;
using UnityEngine;

public class NPCInteract : MonoBehaviour, IINPC
{
    [SerializeField] private String text;
    [SerializeField] private TMP_Text textDialogue;
    [SerializeField] private GameObject textIndicator;

    public void Show()
    {
        Debug.Log("Interact NPC");
        textDialogue.text = text;
        textIndicator.SetActive(false);
    }
}
