using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour
{
    public int ItemID;
    public GameObject ItemObject;
    public void HideItem()
    {
        Inventory.Instance.AddItem(this);
        ItemObject.SetActive(false);
    }
    public void StartMiniGame(string MiniGameName) 
    {
        SceneManager.LoadScene(MiniGameName);
    }
}
