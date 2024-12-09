using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NewDoor : MonoBehaviour
{
    public GameObject ItemsContainerObject; // Объект содержимого всех предметов локации, включая двери.
    private static List<int> InventoryItems;
    private Transform[] ItemsList; // Список всех предметов в ItemsContainerObject
    public static GameObject NameDisplay { get; private set; } // Ссылка на UI текст для отображения имени
    private static GameObject DisplayText;
    private static GameObject DisplayBackground;
    #region Singleton паттерн
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
            InventoryItems = Inventory.Instance.GetItemsID(); // Получаем ID предметов из инвентаря
            ItemsList = ItemsContainerObject.GetComponentsInChildren<Transform>();
            foreach (Transform childObject in ItemsList)
            {
                Item item = childObject.GetComponent<Item>();
                if (item != null)
                {
                    foreach (int itemId in InventoryItems)
                    {
                        if (item.ItemId == itemId) //Если предмет в локации есть в инвентаре, он скрывается
                        {
                            item.gameObject.SetActive(false);
                        }
                        else // иначе добавляется в ItemsToShow 
                        {
                        }
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
        // Обновляем размер фона в соответствии с размером текста
        RectTransform textRectTransform = DisplayText.GetComponent<RectTransform>();
        RectTransform BGRectTransform = DisplayBackground.GetComponent<RectTransform>();
        Vector2 textSize = new Vector2(textRectTransform.rect.width, textRectTransform.rect.height);

        // Добавляем немного отступов
        float padding = 10f;
        BGRectTransform.sizeDelta = new Vector2(textSize.x + padding, textSize.y + padding);
    }
    private Vector2 GetRectTransformScreenSize(RectTransform rect)
    {
        // Массив для получения углов RectTransform
        Vector3[] worldCorners = new Vector3[4];
        rect.GetWorldCorners(worldCorners);

        // Преобразование углов в экранные координаты
        Vector2 bottomLeft = Camera.main.WorldToScreenPoint(worldCorners[0]);
        Vector2 topRight = Camera.main.WorldToScreenPoint(worldCorners[2]);

        // Вычисление ширины и высоты
        float width = topRight.x - bottomLeft.x;
        float height = topRight.y - bottomLeft.y;

        return new Vector2(width, height);
    }
    void Update()
    {
        // Получаем позицию мыши на экране
        Vector3 mousePosition = Input.mousePosition;
        RectTransform textRectTransform = DisplayText.GetComponent<RectTransform>();
        
        // Определяем ширину и высоту экрана
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Проверяем, насколько близко курсор к краям экрана
        Vector2 TextScreenSize = GetRectTransformScreenSize(textRectTransform);
        float textWidth = TextScreenSize.x;
        float textHeight = TextScreenSize.y;
        // Если курсор близок к правому краю, размещаем подсказку слева от курсора
        if (mousePosition.x + textWidth / 2 > screenWidth / 2)
        {
            mousePosition.x -= textWidth / 2;
        }
        // Если курсор близок к левому краю, размещаем подсказку справа от курсора
        else
        {
            mousePosition.x += textWidth / 2;
        }

        // Если курсор близок к верхнему краю, размещаем подсказку ниже курсора
        if (mousePosition.y + textHeight / 2 > screenHeight / 2)
        {
            mousePosition.y -= textHeight;
        }
        // Если курсор близок к нижнему краю, размещаем подсказку выше курсора
        else
        {
            mousePosition.y += textHeight;
        }
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        // Перемещаем объект к позиции мыши
        NameDisplay.transform.position = mousePosition;
        UpdateDisplaySize();
    }
}
