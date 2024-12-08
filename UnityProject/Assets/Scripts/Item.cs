using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    /* (Вроде бы)
     * Для работы скрипта на сцене нужно разместить объект EventSystem(находится во вкладке UI)
     * В камере разместить компонент Physics 2D Raycaster
     */

    public string Name;
    public int ItemId;
    //public bool Necessary;
    public Sprite InventorySprite;
    public Sprite HighlightSprite; // Подсвеченная текстура
    public Sprite NoHighlightSprite; // Обычная текстура
    private Image ImageObject;

    // Start is called before the first frame update
    void Start()
    {
        ImageObject = GetComponent<Image>();
        // Установка исходной текстуры
        if (ImageObject.sprite == null)
        {
            ImageObject.sprite = NoHighlightSprite;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //HighlightItem();
        ImageObject.sprite = HighlightSprite;
        NewDoor.ShowDisplay(Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //UnhighlightItem();
        ImageObject.sprite = NoHighlightSprite;
        NewDoor.HideDisplay();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //HideItem();
        SoundManager.Instance.PlaySound(SoundManager.SoundClip.ItemPick);
        gameObject.SetActive(false);
        Inventory.Instance.AddItem(this);
        NewDoor.HideDisplay();
    }
    // Скрывает предмет и вызывает AddItem(int ItemId) для добавления этого предмета в инвентарь.
    private void HideItem()
    {
    }

    // Меняет текстуру предмета на подсвеченный вариант при наведении на него мышкой.
    private void HighlightItem()
    {
    }
}
