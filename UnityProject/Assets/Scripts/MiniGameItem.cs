using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameItem : MonoBehaviour
{
    public int MGItemID;
    public bool IsGood;
    public GameObject MGItem;
    //To do// �� � ����� ��� ����-���� �� �����?
    public void HideItem()
    {
        Destroy(MGItem);
    }
}
