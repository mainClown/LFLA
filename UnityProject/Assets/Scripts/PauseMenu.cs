using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void OpenMainMenu() 
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void Restart() 
    {
        Inventory.Instance.StartNewInventory();
        SceneManager.LoadScene("BedroomScene");
    }
}
