using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NewDoor : MonoBehaviour
{
    public GameObject ItemsContainerObject;
    private static List<int> InventoryItems;
    private static List<int> UsedItems;
    private Transform[] ItemsList;
    public static GameObject NameDisplay { get; private set; }
    private static GameObject DisplayText;
    private static GameObject DisplayBackground;
    #region Singleton
    private void Awake()
    {
        GameObject UICanvas = GameObject.Find("UICanvas");

        if (UICanvas != null)
        {
            NameDisplay = UICanvas.transform.Find("NameDisplay").gameObject; 
            DisplayText = NameDisplay.transform.Find("DisplayText").gameObject;
            DisplayBackground = NameDisplay.transform.Find("DisplayBackground").gameObject;
            NameDisplay.gameObject.SetActive(false);
        }
    }
    #endregion

    private void Start()
    {
        Camera mainCamera = Camera.main;
        Inventory.Instance.GetComponent<Canvas>().worldCamera = mainCamera;
        if (ItemsContainerObject != null)
        {
            InventoryItems = Inventory.Instance.GetSelectedItemsID();
            UsedItems = Inventory.Instance.GetUsedItemsID();
            ItemsList = ItemsContainerObject.GetComponentsInChildren<Transform>();
            foreach (Transform childObject in ItemsList)
            {
                Item item = childObject.GetComponent<Item>();
                if (item != null)
                {
                    if (item.sceneToLoad != "")
                    {
                        item.gameObject.SetActive(!UsedItems.Contains(item.ItemId));
                    }
                    else 
                    {
                        item.gameObject.SetActive(!InventoryItems.Contains(item.ItemId));
                    }
                }
            }
        }
    }
    public static void ShowDisplay(string newText)
    {
        DisplayText.GetComponent<TextMeshProUGUI>().text = newText;
        NameDisplay.gameObject.SetActive(true);
        UpdateDisplaySize();
    }
    public static void HideDisplay() 
    {
        if (NameDisplay)
        {
            NameDisplay.gameObject.SetActive(false);
        }
    }
    private static void UpdateDisplaySize()
    {
        RectTransform textRectTransform = DisplayText.GetComponent<RectTransform>();
        RectTransform BGRectTransform = DisplayBackground.GetComponent<RectTransform>();
        Vector2 textSize = new Vector2(textRectTransform.rect.width, textRectTransform.rect.height);

        float padding = 10f;
        BGRectTransform.sizeDelta = new Vector2(textSize.x + padding, textSize.y + padding);
    }
    private Vector2 GetRectTransformScreenSize(RectTransform rect)
    {
        Vector3[] worldCorners = new Vector3[4];
        rect.GetWorldCorners(worldCorners);

        Vector2 bottomLeft = Camera.main.WorldToScreenPoint(worldCorners[0]);
        Vector2 topRight = Camera.main.WorldToScreenPoint(worldCorners[2]);

        float width = topRight.x - bottomLeft.x;
        float height = topRight.y - bottomLeft.y;

        return new Vector2(width, height);
    }
    void Update()
    {

        Vector3 mousePosition = Input.mousePosition;
        RectTransform textRectTransform = DisplayText.GetComponent<RectTransform>();
        
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Vector2 TextScreenSize = GetRectTransformScreenSize(textRectTransform);
        float textWidth = TextScreenSize.x;
        float textHeight = TextScreenSize.y;

        if (mousePosition.x + textWidth / 2 > screenWidth / 2)
        {
            mousePosition.x -= textWidth / 2;
        }
        else
        {
            mousePosition.x += textWidth / 2;
        }

        if (mousePosition.y + textHeight / 2 > screenHeight / 2)
        {
            mousePosition.y -= textHeight;
        }
        else
        {
            mousePosition.y += textHeight;
        }
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        NameDisplay.transform.position = mousePosition;
        UpdateDisplaySize();
    }
}
