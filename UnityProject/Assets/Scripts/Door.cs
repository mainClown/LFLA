using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //?? Зачем нужен класс ради одной функции ??
    public void OpenLocation(string LocationName)
    {
        SceneManager.LoadScene(LocationName);
    }
}
