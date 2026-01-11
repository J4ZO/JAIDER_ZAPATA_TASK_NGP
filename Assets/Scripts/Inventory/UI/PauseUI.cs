using System;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private InventorySaveManager inventorySaveManager;
    public bool isPaused = false;
    
    public void OpenPauseUI()
    {
        isPaused = true;
        gameObject.SetActive(true);
    }

    public void ClosePauseUI()
    {
        isPaused = false;
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        inventorySaveManager.SaveInventory();
        Application.Quit();
    }
}
