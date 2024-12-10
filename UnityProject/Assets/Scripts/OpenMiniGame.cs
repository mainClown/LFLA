using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneLoaderOnPointer : MonoBehaviour,  IPointerClickHandler
{
    public string sceneToLoad; // �������� ����� ��� ��������
    public GameObject CanvasUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        // �������� ����� ��� �����
       // CanvasUI.SetActive(true);

        SceneManager.LoadScene(sceneToLoad);
    }

 
}
