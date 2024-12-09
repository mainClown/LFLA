using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //NotNecessaryItems убрал потому что а зачем они
    public List<int> NecessaryItems; // айдишники обязательных предметов для открытия этой двери
    public string LocationSceneName; //куда ведёт
    public string LocationDisplay; //что вывести на экран подсказки
    public string NextLocationItems; //да тупо, но строка с айди предметов в следующей локации для TextBubble
    //Почему строка? а не List<int> как NecessaryItems?
    public Sprite HighlightSprite;
    public Sprite NoHighlightSprite;
    private Image DoorImageObject;
    private static List<int> InventoryItems;
    void Start()
    {
        DoorImageObject = GetComponent<Image>();
        // Установка исходной текстуры
        if (DoorImageObject.sprite == null)
        {
            DoorImageObject.sprite = NoHighlightSprite;
        }

    }
    public bool CheckTransition()
    {
        InventoryItems = Inventory.Instance.GetItemsID();
        return InventoryItems.Count >= NecessaryItems.Count && NecessaryItems.All(InventoryItems.Contains);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        DoorImageObject.sprite = HighlightSprite;
        NewDoor.ShowDisplay(LocationDisplay);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DoorImageObject.sprite = NoHighlightSprite;
        NewDoor.HideDisplay();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        NewDoor.HideDisplay();
        OpenLocation();
    }
    public void OpenLocation() 
    {
        if (CheckTransition())
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundClip.DoorCreak);
            if (this.NextLocationItems != "none")
            {
                var NoLocationItemsInMesgs = true;
                List<int> LstItems = this.NextLocationItems.Split(",").Select(int.Parse).ToList();
                if (TextBubble.ItemsToShow.Count > 0)
                {
                    var lst = TextBubble.ItemsToShow;
                    for (int i = 0; i < LstItems.Count; i++)
                    {
                        for (int j = 0; j < lst.Count; j++)
                        {
                            if (LstItems[i] == lst[j])
                               NoLocationItemsInMesgs = false;
                        }
                    }
                }
                if (TextBubble.ItemsToShow.Count > 0)
                {
                    for (int i = 0; i < LstItems.Count; i++)
                    {
                        for (int j = 0; j < InventoryItems.Count; j++)
                        {
                            if (LstItems[i] == InventoryItems[j])
                                NoLocationItemsInMesgs = false;
                        }
                    }
                }
                if (NoLocationItemsInMesgs)
                    TextBubble.Instance.AddItemsToShow(LstItems);
            }
            //Debug.Log(TextBubble.ItemsToShow.Count);
            if (LocationSceneName == "EndingScene")
            {
                Ending.Instance.SetEndingType(true);
                SceneManager.LoadScene(LocationSceneName);
                Destroy(GameObject.Find("UICanvas"));
            }
            else
            {
                SceneManager.LoadScene(LocationSceneName);
            }
        }
        else 
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundClip.ButtonClick);
        }
    }
}
