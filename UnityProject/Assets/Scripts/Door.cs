using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    //public GameObject BlockScreen; //При асинхронной загрузке сцены, чтобы нельзя было нажать на другие двери (предметы).
    //NotNecessaryItems убрал потому что а зачем они
    public List<int> NecessaryItems; // айдишники обязательных предметов для открытия этой двери
    private static List<int> InventoryItems;
    //Я пытался и так и сяк, эти две функции не разделимы, увынск
    public bool CheckTransition()
    {
        InventoryItems = Inventory.Instance.GetItemsID();
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
    //Если надо небольшую задержку перед переходом или задержку по условию - использовать асинхронный метод и корутину

    //private IEnumerator OpenLocation(string LocationName)
    //{
    //    if (CheckTransition())
    //    {
    //        BlockScreen.SetActive(true);
    //        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LocationName);

    //        // Устанавливаем allowSceneActivation в false, чтобы избежать автоматической активации
    //        asyncLoad.allowSceneActivation = false;

    //        // Ждём, пока загрузка не завершится (пока progress != 1)
    //        while (!asyncLoad.isDone)
    //        {
    //            if (asyncLoad.progress >= 0.9f)
    //            {
    //                // Ждём 3 секунды
    //                yield return new WaitForSeconds(2);

    //                // Переключаемся на загруженную сцену
    //                asyncLoad.allowSceneActivation = true;
    //            }
    //            yield return null;
    //        }
    //    }
    //    else
    //    {
    //        //TextBubble.Instance.ShowAndHideTextBubble() 
    //        //Звук-пук хз чето воспроизвести
    //    }
    //}
    //public void TryToOpenDoor(string LocationName) 
    //{
    //    StartCoroutine(OpenLocation(LocationName));
    //}
}
