using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject CanvasUI;
    public void OpenMainMenu() 
    {
        Destroy(CanvasUI);
        SceneManager.LoadScene("MainMenuScene");
    }
    public void Restart() 
    {
        Destroy(CanvasUI);
        Inventory.Instance.NewInventory();
        TextBubble.Instance.NewItemsToShow();
        SceneManager.LoadScene("BedroomScene");
    }
}
