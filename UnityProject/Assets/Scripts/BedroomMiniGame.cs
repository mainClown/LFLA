using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.XR;

public class BedroomMinigame : MonoBehaviour
{
    public Button CloseButton;
    public Sprite InventorySprite;
    public GameObject textOrig;
    public GameObject textNew;
    private int CurrentHits = 0;
    int hitsToWin = 6;

    // Start is called before the first frame update
    void Start()
    {
        Camera mainCamera = Camera.main;
        Inventory.Instance.GetComponent<Canvas>().worldCamera = mainCamera;
        CloseButton.onClick.AddListener(CloseBedroomMiniGame);
        Timer.Instance.OnMiniGameEnd += CloseBedroomMiniGame;
        TextBubble.Instance.HideTextBubble();
        StartCoroutine(HideObject());
    }
    void Update()
    {
        CheckHit();
    }

    private void CloseBedroomMiniGame()
    {
        Timer.Instance.ResetMiniGameTimer();
        SceneManager.LoadScene("BedroomScene");
        TextBubble.Instance.StartAgain();
    }
    void CheckHit()
    {
        CurrentHits = 0;
        for (int i = 1; i <= 6; i++)
        {
            string name = "place" + i.ToString();
            var obj = GameObject.Find(name);
            if (obj != null)
            {
                var id = obj.GetComponent<MiniGamePlace>().Id;
                var itemId = obj.GetComponent<MiniGamePlace>().ItemId;
                if (id == itemId)
                    CurrentHits += 1;
            }
        }
        if (CurrentHits == hitsToWin)
        {
            Win();
        }
    }

    private void Win() 
    {

        GameObject itemObject = new GameObject("Report");
        Item item = itemObject.AddComponent<Item>();

        item.ItemId = 4;
        item.Name = "������";
        item.InventorySprite = InventorySprite;

        // ���������� � ���������
        Inventory.Instance.AddItem(item);
        CloseBedroomMiniGame();
    }
    IEnumerator HideObject()
    {
        yield return new WaitForSeconds(15.0f);
        textOrig.SetActive(false);
        textNew.SetActive(true);
    }

}
