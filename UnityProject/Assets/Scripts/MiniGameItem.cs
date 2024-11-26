using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameItem : MonoBehaviour
{
    public int MGItemID;
    public bool IsGood;
    public GameObject MGItem;
    //To do// ћб к чЄрту эту мини-игру на кухне?
    private void Awake()
    {
        //AutoInstance for GameObject in KitchenMiniGame (Ёто даже звучит страшно)
    }
    public void HideItem()
    {
        Destroy(MGItem);
    }
}
