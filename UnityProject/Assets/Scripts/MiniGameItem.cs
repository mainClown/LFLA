using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameItem : MonoBehaviour
{
    public int Id;
    public string Name;
    public bool IsGood = false;
    public Text nameDisplay; // —сылка на UI текст дл€ отображени€ имени

    public void HideItem(int Id)
    {
        GameObject.SetActive(false);
    }
}
