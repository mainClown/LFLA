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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowEnding(bool inTime, string EndingText)
    {

    }

    public string GenerateEndingText(bool inTime, string MainTextFile, string ItemsTextFile, List<Item> collectedItems)
    {

    }
}
