using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameItem : MonoBehaviour
{
    public int MGItemID;
    public bool IsGood;
    public GameObject MGItem;
    //To do// �� � ����� ��� ����-���� �� �����?
    private void Awake()
    {
        //AutoInstance for GameObject in KitchenMiniGame (��� ���� ������ �������)
    }
    public void HideItem()
    {
        Destroy(MGItem);
    }
}
