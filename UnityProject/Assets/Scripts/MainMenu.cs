using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        Inventory.Instance.NewInventory();
        TextBubble.Instance.NewItemsToShow();
        SceneManager.LoadScene("BedroomScene");    
    }
    public void Exit() 
    {
        Application.Quit();
    }
}
