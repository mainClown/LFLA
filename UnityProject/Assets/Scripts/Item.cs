using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    /* (Вроде бы)
     * Для работы скрипта на сцене нужно разместить объект EventSystem(находится во вкладке UI)
     * В камере разместить компонент Physics 2D Raycaster
     */
    public string Name;
    public int ItemId;
    public bool Necessary;
    public Sprite HighliteFileName; // Подсвеченная текстура
    public Sprite NoHighliteFileName; // Обычная текстура
    private SpriteRenderer spriteRenderer;
    public Text nameDisplay; // Ссылка на UI текст для отображения имени

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Устанавка исходной текстуры
        if(spriteRenderer == null)
        {
            spriteRenderer.sprite = NoHighlightSprite;
        }   
    }

    private void OnMouseEnter()
    {
        HighliteItem(ItemId);
        nameDisplay.text = Name; // Установка текста
        nameDisplay.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = NoHighlightSprite;
        nameDisplay.gameObject.SetActive(false); // Скрытие текста
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Скрывает предмет и вызывает AddItem(int ItemId) для добавления этого предмета в инвентарь.
    void HideItem(int ItemId)
    {
        GameObject.SetActive(false);
        Inventory.AddItem(ItemId);
    }

    // Меняет текстуру предмета на подсвеченный вариант при наведении на него мышкой.
    void HighliteItem(int ItemId)
    {
        spriteRenderer.sprite = HighlightSprite;
    }
}
