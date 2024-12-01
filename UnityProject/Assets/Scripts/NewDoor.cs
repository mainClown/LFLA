using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewDoor : MonoBehaviour
{
    public GameObject ItemsContainerObject; // ������ ����������� ���� ��������� �������, ������� �����.
    private static List<int> InventoryItems;
    private Transform[] ItemsList; // ������ ���� ��������� � ItemsContainerObject

    //�� �������� � �������, ������ �� UI ������, �������� ��� ��������� ������� �������� � ���������� ��������� � ItemsToShow
    private void Start()
    {
        Camera mainCamera = Camera.main;
        Inventory.Instance.GetComponent<Canvas>().worldCamera = mainCamera;
        if (ItemsContainerObject != null)
        {
            InventoryItems = Inventory.Instance.GetItemsID(); // �������� ID ��������� �� ���������
            ItemsList = ItemsContainerObject.GetComponentsInChildren<Transform>();
            foreach (Transform childObject in ItemsList)
            {
                Item item = childObject.GetComponent<Item>();
                if (item != null)
                {
                    foreach (int itemID in InventoryItems)
                    {
                        if (item.ItemID == itemID) //���� ������� � ������� ���� � ���������, �� ����������
                        {
                            item.gameObject.SetActive(false);
                        }
                        else // ����� ����������� � ItemsToShow 
                        {
                            TextBubble.Instance.AddItemsToShow(item.ItemID);
                        }
                    }
                }
            }
        }
    }

    //� ������ ��� ������, �� � Door.cs
}
