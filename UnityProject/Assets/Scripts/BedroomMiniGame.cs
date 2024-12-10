using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class BedroomMinigame : MonoBehaviour
{
    public Button locationButton;
    public string LocationSceneName;
    public GameObject CanvasUI;

    // Start is called before the first frame update
    void Start()

    {
        //CanvasUI.SetActive(false);
        if (locationButton != null)
        {
            
            locationButton.onClick.AddListener(OpenLocation);
           // Debug.LogError("AddListener");
        }
        else
        {
            Debug.LogError("Button with name 'YourButtonName' not found!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenLocation()
    {
        Debug.LogError("Button with name!");


        //  SoundManager.Instance.PlaySound(SoundManager.SoundClip.DoorCreak);
        SceneManager.LoadScene(LocationSceneName);
        //CanvasUI.SetActive(true);

    }
}
