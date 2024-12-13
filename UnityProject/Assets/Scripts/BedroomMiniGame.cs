using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class BedroomMinigame : MonoBehaviour
{
    public Button CloseButton;
    public Sprite InventorySprite;

    // Start is called before the first frame update
    void Start()
    {
        Camera mainCamera = Camera.main;
        Inventory.Instance.GetComponent<Canvas>().worldCamera = mainCamera;
        CloseButton.onClick.AddListener(CloseBedroomMiniGame);
        Timer.Instance.OnMiniGameEnd += CloseBedroomMiniGame;
    }  
    private void CloseBedroomMiniGame()
    {
        Timer.Instance.ResetMiniGameTimer();
        SceneManager.LoadScene("BedroomScene");
    }
    private void CheckWin() 
    {
        GameObject itemObject = new GameObject("Report");
        Item item = itemObject.AddComponent<Item>();

        item.ItemId = 4;
        item.Name = "Доклад";
        item.InventorySprite = InventorySprite;

        // Добавление в инвентарь
        Inventory.Instance.AddItem(item);
        CloseBedroomMiniGame();
    }
}
