using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //NotNecessaryItems убрал потому что а зачем они
    public List<int> NecessaryItems; // айдишники обязательных предметов
    private static List<int> InventoryItems;
    //Я пытался и так и сяк, эти две функции не разделимы, увынск
    public bool CheckTransition()
    {
        InventoryItems = Inventory.Instance.GetItems();
        return InventoryItems.Count >= NecessaryItems.Count && NecessaryItems.All(InventoryItems.Contains);
    }
    public void OpenLocation(string LocationName)
    {
        if (CheckTransition())
        {
            SceneManager.LoadScene(LocationName);
        }
        else
        {
            //TextBubble.Instance.ShowAndHideTextBubble() 
            //Звук-пук хз чето воспроизвести
        }
    }
}
