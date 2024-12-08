using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;

public class Ending : MonoBehaviour
{
    public string EndingText;
    public string MainTextFile = "EndingTexts.csv";
    public string ItemsTextFile = "EndingTextsForItems.csv";

    private Dictionary<string, string> mainTextDictionary = new Dictionary<string, string>();
    private Dictionary<string, ItemText> itemsDictionary = new Dictionary<string, ItemText>();

    [System.Serializable]
    public class ItemText
    {
        public string Taken;
        public string NotTaken;

        public ItemText(string taken, string notTaken)
        {
            Taken = taken;
            NotTaken = notTaken;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadMainTextFile();
        LoadItemsTextFile();
    }

    private void LoadMainTextFile()
    {
        string filePath = Path.Combine(Application.dataPath, "_Assets", MainTextFile);
        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] columns = line.Split(';');

            if (columns.Length >= 2)
            {
                mainTextDictionary[columns[0]] = columns[1];
            }
        }
    }

    private void LoadItemsTextFile()
    {
        string filePath = Path.Combine(Application.dataPath, "_Assets", ItemsTextFile);
        string[] lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] columns = lines[i].Split(';');

            if (columns.Length >= 3)
            {
                string item = columns[0];
                string taken = columns[1];
                string notTaken = columns[2];

                itemsDictionary[item] = new ItemText(taken, notTaken);
            }
        }
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
        EndingText = GenerateEndingText(inTime, collectedItems);
    }

    public string GenerateEndingText(bool inTime, List<Item> collectedItems)
    {
        StringBuilder endingTextBuilder = new StringBuilder();

        if (inTime)
        {
            endingTextBuilder.Append(mainTextDictionary["InTime"]);

            // 7 обязательных предметов, 9 пкфк
            int numberOfItems = collectedItems.Count;

            if (numberOfItems == 7)
            {
                endingTextBuilder.Append(mainTextDictionary["ItemsNotTaken"]);
            }
            else if (numberOfItems < 11)
            {
                endingTextBuilder.Append(mainTextDictionary["SomeItemsTaken"]);
            }
            else
            {
                endingTextBuilder.Append(mainTextDictionary["AllItemsTaken"]);
            }

            foreach (var row in itemsDictionary)
            {
                string key = row.Key;
                ItemText itemData = row.Value;

                if (collectedItems.Any(item => item.Name == key))
                {
                    endingTextBuilder.Append(itemData.Taken);
                }
                else
                {
                    endingTextBuilder.Append(itemData.NotTaken);
                }
            }
        }
        else
        {
            endingTextBuilder.Append(mainTextDictionary["Late"]);
        }

        return endingTextBuilder.ToString(); ;
    }

}
