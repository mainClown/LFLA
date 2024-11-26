using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewDoor : MonoBehaviour
{
    public List<Item> NecessaryItems;
    public List<Item> UnnecessaryItems;

    public GameObject ItemsContainerObject;
    private static List<int> LocationItems;

    private Transform[] ItemsList;
    //По переходу в локацию, скрывает все собранные игроком предметы
    private void Awake()
    {
        if (ItemsContainerObject != null)
        {
            LocationItems = Inventory.Instance.GetItems();
            ItemsList = ItemsContainerObject.GetComponentsInChildren<Transform>();
            foreach (Transform childObject in ItemsList)
            {
                Item item = childObject.GetComponent<Item>();
                if (item != null)
                {
                    foreach (int itemID in LocationItems)
                    {
                        if (item.ItemID == itemID)
                        {
                            item.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
        //TextBubble.Instance.AddItemsToShow(NecessaryItems);
        //TextBubble.Instance.AddItemsToShow(UnnecessaryItems);
    }

    public void CheckTransition() 
    {
        //Пока не чекает ничего
    }
    //Перенёс функцию из Door.cs
    public void OpenLocation(string LocationName)
    {
        SceneManager.LoadScene(LocationName);
    }
}
