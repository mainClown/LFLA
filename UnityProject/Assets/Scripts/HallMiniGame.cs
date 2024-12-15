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

    //Problems
    private string СurrentProblem;
    private string currentAnswer = "";

    // Score Management
    int CorrectAnswersToWin = 5;
    private int CorrectAnswers = 0;

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
        СurrentProblem = randomEquation.Key;
        equationText.text = СurrentProblem;
        currentAnswer = "";
        answerText.text = currentAnswer;
    }
    public void CloseHallMiniGame()
    {
        Timer.Instance.ResetMiniGameTimer();
        TextBubble.Instance.StartAgain();
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
            int correctAnswer = Problems[СurrentProblem]; //берем правильный ответ из словаря по ключу
            Problems.Remove(СurrentProblem);
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
            item.Name = "Калькулятор";
            item.InventorySprite = InventorySprite;

            // Добавление в инвентарь
            Inventory.Instance.AddItem(item);
            CloseHallMiniGame();
        }
       
    }

}
