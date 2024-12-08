using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public string EndingText;
    private string MainTextFile = "EndingTexts.csv";
    private string ItemsTextFile = "EndingTextsForItems.csv";

    // Start is called before the first frame update
    void Start()
    {
    }

    //    public static Ending Instance { get; private set; }
    //    #region Singleton паттерн
    //    private void Awake()
    //    {
    //        if (Instance != null && Instance != this)
    //        {
    //            Destroy(this);
    //        }
    //        else
    //        {
    //            Instance = this;
    //            DontDestroyOnLoad(target: this);
    //        }
    //    }
    //    #endregion

    public void ShowEnding(bool inTime, string EndingText)
    {

    }

    public string GenerateEndingText(bool inTime, string MainTextFile, string ItemsTextFile, List<Item> collectedItems)
    {
        return null;
    }

}
