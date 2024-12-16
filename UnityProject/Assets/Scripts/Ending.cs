using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Ending : MonoBehaviour
{
    private string EndingText;
    public string MainTextFile1 = "EndingTexts";
    public string ItemsTextFile1 = "EndingTextsForItems";

    private TMP_Text TextField;
    private Dictionary<string, string> mainTextDictionary = new Dictionary<string, string>();
    private Dictionary<string, ItemText> itemsDictionary = new Dictionary<string, ItemText>();
    private bool inTime; //���������� ��� ����� ������� ���������� ����� ������, ����� ��� ��������� ��� �������.
    public static Ending Instance { get; private set; }
    #region Singleton �������
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(target: this);
        }
    }
    #endregion

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
        SceneManager.sceneLoaded += OnSceneLoaded; //�������� �� ������� sceneLoaded
        LoadMainTextFile();
        LoadItemsTextFile();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "EndingScene")
        {
            ShowEnding();
        }
    }
    private void LoadMainTextFile()
    {
        TextAsset mainTextAsset = Resources.Load<TextAsset>("CSVFiles/" + MainTextFile1);

        if (mainTextAsset != null)
        {
            string[] lines = mainTextAsset.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                string[] columns = line.Split(';');

                if (columns.Length >= 2)
                {
                    mainTextDictionary[columns[0]] = columns[1];
                }
            }
        }
        else
        {
            Debug.LogError("Main text file not found in Resources.");
        }
    }

    private void LoadItemsTextFile()
    {
        TextAsset itemsTextAsset = Resources.Load<TextAsset>("CSVFiles/" + ItemsTextFile1);

        if (itemsTextAsset != null)
        {
            string[] lines = itemsTextAsset.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

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
        else
        {
            Debug.LogError("Items text file not found in Resources.");
        }
    }
    //private void LoadMainTextFile()
    //{
    //    string filePath = Path.Combine(Application.dataPath, "_Assets", MainTextFile);
    //    string[] lines = File.ReadAllLines(filePath);

    //    foreach (string line in lines)
    //    {
    //        string[] columns = line.Split(';');

    //        if (columns.Length >= 2)
    //        {
    //            mainTextDictionary[columns[0]] = columns[1];
    //        }
    //    }
    //}

    //private void LoadItemsTextFile()
    //{
    //    string filePath = Path.Combine(Application.dataPath, "_Assets", ItemsTextFile);
    //    string[] lines = File.ReadAllLines(filePath);

    //    for (int i = 1; i < lines.Length; i++)
    //    {
    //        string[] columns = lines[i].Split(';');

    //        if (columns.Length >= 3)
    //        {
    //            string item = columns[0];
    //            string taken = columns[1];
    //            string notTaken = columns[2];

    //            itemsDictionary[item] = new ItemText(taken, notTaken);
    //        }
    //    }
    //}
    private void ShowEnding() // ���������� ShowEnding � ��� ���� ���������, � �� ��� �� ������ ������� - � ���������� ���������� � ����� ������,
                              // ������ ��� ���� ��� ������������ ��, � ����� ����� �� ������ �������� OnSceneLoaded
    {
        EndingText = GenerateEndingText(inTime, Inventory.SelectedItems);
        if (EndingText == null)
            EndingText = "";

        GameObject EndingCanvas = GameObject.Find("EndingCanvas");
        if (EndingCanvas)
        {
            GameObject textB = EndingCanvas.transform.Find("EndingObj").gameObject;
            TextField = textB.transform.Find("EndText").gameObject.GetComponent<TMP_Text>();
        }
        TextField.GetComponent<TextMeshProUGUI>().text = EndingText;

    }
    public void SetEndingType(bool inTime) //��������� ������ inTime ������� ��������� �����,
                                              //�� ������ ������ ��� ����� �������� ������.
    {
        this.inTime = inTime;
    }
    public string GenerateEndingText(bool inTime, List<Item> collectedItems)
    {
        StringBuilder endingTextBuilder = new StringBuilder();

        if (inTime)
        {
            endingTextBuilder.Append(mainTextDictionary["InTime"]).Append(" ");

            // 7 ������������ ���������, 9 ����
            int numberOfItems = collectedItems.Count;

            if (numberOfItems == 7)
            {
                endingTextBuilder.Append(mainTextDictionary["ItemsNotTaken"]).Append(" ");
            }
            else if (numberOfItems < 11)
            {
                endingTextBuilder.Append(mainTextDictionary["SomeItemsTaken"]).Append(" ");
            }
            else
            {
                endingTextBuilder.Append(mainTextDictionary["AllItemsTaken"]).Append(" ");
            }

            foreach (var row in itemsDictionary)
            {
                string key = row.Key;
                ItemText itemData = row.Value;

                if (collectedItems.Any(item => item.Name == key))
                {
                    endingTextBuilder.Append(itemData.Taken).Append(" ");
                }
                else
                {
                    endingTextBuilder.Append(itemData.NotTaken).Append(" ");
                }
            }
        }
        else
        {
            endingTextBuilder.Append(mainTextDictionary["Late"]).Append(" ");
        }

        return endingTextBuilder.ToString().Trim();
    }

}