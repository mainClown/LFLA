using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewDoor : MonoBehaviour
{
    public GameObject ItemsContainerObject; // Объект содержимого всех предметов локации, включая двери.
    private static List<int> InventoryItems;

    private Transform[] ItemsList; // Список всех предметов в ItemsContainerObject
    //По переходу в локацию, скрывает все собранные игроком предметы и оставшиеся добавляет в ItemsToShow
    private void Awake()
    {
        if (ItemsContainerObject != null)
        {
            InventoryItems = Inventory.Instance.GetItems(); // Получаем ID предметов из инвентаря
            ItemsList = ItemsContainerObject.GetComponentsInChildren<Transform>();
            foreach (Transform childObject in ItemsList)
            {
                Item item = childObject.GetComponent<Item>();
                if (item != null)
                {
                    foreach (int itemID in InventoryItems)
                    {
                        if (item.ItemID == itemID) //Если предмет в локации есть в инвентаре, он скрывается
                        {
                            item.gameObject.SetActive(false);
                        }
                        else // иначе добавляется в ItemsToShow 
                        {
                            TextBubble.Instance.AddItemsToShow(item.ItemID);
                        }
                    }
                }
            }
        }
    }
    //А больше нет ничего, всё в Door.cs
}
