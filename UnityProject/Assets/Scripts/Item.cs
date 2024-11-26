using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ItemID;
    public GameObject ItemObject;
    public void HideItem()
    {
        Inventory.Instance.AddItem(this);
        ItemObject.SetActive(false);
    }
}
