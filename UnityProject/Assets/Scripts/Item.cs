using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public string Name;
    public int ItemId;
    public string sceneToLoad; // Название сцены для загрузки
    public int MiniGameTimeSeconds;
    public Sprite InventorySprite;
    public Sprite HighlightSprite; // Подсвеченная текстура
    public Sprite NoHighlightSprite; // Обычная текстура
    private Image ImageObject;

    void Start()
    {
        ImageObject = GetComponent<Image>();
        // Установка исходной текстуры
        if (ImageObject != null)
        {
            if (ImageObject.sprite == null)
            {
                ImageObject.sprite = NoHighlightSprite;
            }
        }
        //Если не будет работать закомментировать
        if (HallMiniGame.playerWon || BedroomMinigame.playerWon || KitchenMiniGame.playerWon || BathroomMinigame.playerWon)
            {
            //    Debug.Log("Игрок победил в мини-игре!");
            Inventory.Instance.UseItem(this);
            HallMiniGame.playerWon = BedroomMinigame.playerWon = KitchenMiniGame.playerWon = BathroomMinigame.playerWon = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ImageObject.sprite = HighlightSprite;
        NewDoor.ShowDisplay(Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ImageObject.sprite = NoHighlightSprite;
        NewDoor.HideDisplay();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (sceneToLoad == "")
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundClip.ItemPick);
            gameObject.SetActive(false);
            Inventory.Instance.AddItem(this);
            TextBubble.Instance.DeleteItemToShow(this.ItemId);
            NewDoor.HideDisplay();
        }
        else 
        {
           
                // Inventory.Instance.UseItem(this);
                Timer.Instance.SetMiniGameTimer(MiniGameTimeSeconds);
            NewDoor.HideDisplay();
            SceneManager.LoadScene(sceneToLoad); 
            
        }
    }
}
