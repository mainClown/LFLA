using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    //public GameObject BlockScreen; //��� ����������� �������� �����, ����� ������ ���� ������ �� ������ ����� (��������).
    //NotNecessaryItems ����� ������ ��� � ����� ���
    public List<int> NecessaryItems; // ��������� ������������ ��������� ��� �������� ���� �����
    private static List<int> InventoryItems;
    //� ������� � ��� � ���, ��� ��� ������� �� ���������, ������
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
            //����-��� �� ���� �������������
        }
    }
    //���� ���� ��������� �������� ����� ��������� ��� �������� �� ������� - ������������ ����������� ����� � ��������

    //private IEnumerator OpenLocation(string LocationName)
    //{
    //    if (CheckTransition())
    //    {
    //        BlockScreen.SetActive(true);
    //        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LocationName);

    //        // ������������� allowSceneActivation � false, ����� �������� �������������� ���������
    //        asyncLoad.allowSceneActivation = false;

    //        // ���, ���� �������� �� ���������� (���� progress != 1)
    //        while (!asyncLoad.isDone)
    //        {
    //            if (asyncLoad.progress >= 0.9f)
    //            {
    //                // ��� 3 �������
    //                yield return new WaitForSeconds(2);

    //                // ������������� �� ����������� �����
    //                asyncLoad.allowSceneActivation = true;
    //            }
    //            yield return null;
    //        }
    //    }
    //    else
    //    {
    //        //TextBubble.Instance.ShowAndHideTextBubble() 
    //        //����-��� �� ���� �������������
    //    }
    //}
    //public void TryToOpenDoor(string LocationName) 
    //{
    //    StartCoroutine(OpenLocation(LocationName));
    //}
}
