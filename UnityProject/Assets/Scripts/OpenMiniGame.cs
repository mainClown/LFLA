using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneLoaderOnPointer : MonoBehaviour,  IPointerClickHandler
{
    public string sceneToLoad; // Название сцены для загрузки
    public GameObject CanvasUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Загрузка сцены при клике
       // CanvasUI.SetActive(true);

        SceneManager.LoadScene(sceneToLoad);
    }

 
}
