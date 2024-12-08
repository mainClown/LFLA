using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject CanvasUI;
    public GameObject PauseUI;
    public void PauseGame() 
    {
        PauseUI.SetActive(true);
        Timer.Instance.TogglePause();
    }
    public void UnPauseGame() 
    {
        PauseUI.SetActive(false);
        Timer.Instance.TogglePause();
    }
    public void OpenMainMenu() 
    {
        Destroy(CanvasUI);
        SceneManager.LoadScene("MainMenuScene");
    }
    public void Restart() 
    {
        Destroy(CanvasUI);
        SceneManager.LoadScene("BedroomScene");
    }
}
