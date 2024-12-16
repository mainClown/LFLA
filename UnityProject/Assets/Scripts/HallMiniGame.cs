using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class HallMiniGame : MonoBehaviour
{
    // Game Objects and Components
    public Button CloseButton;
    public TMP_Text equationText;
    public TMP_Text answerText;
    public Button[] numberButtons;
    public Button enterButton;
    public Sprite InventorySprite;
    public GameObject pauseBtn;
    public GameObject inventoryBtn;

    //Problems
    private string —urrentProblem;
    private string currentAnswer = "";

    // Score Management
    int CorrectAnswersToWin = 5;
    private int CorrectAnswers = 0;

    public static bool playerWon = false;

    private Dictionary<string, int> Problems = new Dictionary<string, int>()
    {
        {"7+4", 11},
        {"9-2", 7},
        {"5*3", 15},
        {"12/3", 4},
        {"6+8", 14},
        {"34+43", 77},
        {"60*24", 1440},
        {"84/28", 3},
        {"73-29", 44},
        {"568/71", 8},
    };

    
    void Start()

    {
        pauseBtn = (GameObject.Find("PauseBtn"));
        inventoryBtn = (GameObject.Find("InventoryBtn"));
        (pauseBtn).SetActive(false);
        (inventoryBtn).SetActive(false);
        Camera mainCamera = Camera.main;
        Inventory.Instance.GetComponent<Canvas>().worldCamera = mainCamera;
        CloseButton.onClick.AddListener(CloseHallMiniGame);
        Timer.Instance.OnMiniGameEnd += CloseHallMiniGame;
        TextBubble.Instance.HideTextBubble();
        foreach (Button button in numberButtons)
        {
            int number = int.Parse(button.name); 
            button.onClick.AddListener(() => AddDigit(number));
        }

        enterButton.onClick.AddListener(CheckAnswers);
        LoadNextProblem();
    }
    void LoadNextProblem()
    {
        if (Problems.Count == 0)
        {
            Debug.Log("No more equations!");
            return;
        }

        KeyValuePair<string, int> randomEquation = Problems.ElementAt(Random.Range(0, Problems.Count));
        —urrentProblem = randomEquation.Key;
        equationText.text = —urrentProblem;
        currentAnswer = "";
        answerText.text = currentAnswer;
    }
    public void CloseHallMiniGame()
    {
        Timer.Instance.ResetMiniGameTimer();
        TextBubble.Instance.StartAgain();
        (pauseBtn).SetActive(true);
        (inventoryBtn).SetActive(true);
        SceneManager.LoadScene("HallScene");
    }
    void AddDigit(int digit)
    {
        currentAnswer += digit.ToString();
        answerText.text = currentAnswer;
    }

    void CheckAnswers()
    {
        if (string.IsNullOrEmpty(currentAnswer)) return;

        int userAnswer;
        if (int.TryParse(currentAnswer, out userAnswer))
        {
            int correctAnswer = Problems[—urrentProblem]; //·ÂÂÏ Ô‡‚ËÎ¸Ì˚È ÓÚ‚ÂÚ ËÁ ÒÎÓ‚‡ˇ ÔÓ ÍÎ˛˜Û
            Problems.Remove(—urrentProblem);
            if (userAnswer == correctAnswer)
            {
                CorrectAnswers++;
                CheckWin();
                
            }
            else
            {
                CorrectAnswers--;
            }
            LoadNextProblem();
        }
        else
        {
            Debug.LogError("Invalid user input.");
        }
    }

    

    void CheckWin()
    {
        if (CorrectAnswers >= CorrectAnswersToWin)
        {
            GameObject itemObject = new GameObject("Calculator");
            Item item = itemObject.AddComponent<Item>();

            item.ItemId = 11;
            item.Name = " ‡Î¸ÍÛÎˇÚÓ";
            item.InventorySprite = InventorySprite;

            // ƒÓ·‡‚ÎÂÌËÂ ‚ ËÌ‚ÂÌÚ‡¸
            Inventory.Instance.AddItem(item);
            playerWon = true;
            CloseHallMiniGame();
        }
       
    }

}
