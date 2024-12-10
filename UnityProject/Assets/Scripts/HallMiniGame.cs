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
    public Button locationButton;
    public string LocationSceneName;
    public GameObject CanvasUI;
    public TMP_Text equationText;
    public TMP_Text answerText;
    public Button[] numberButtons;
    public Button enterButton;


    //Problems
    private string ÑurrentProblem;
    private string currentAnswer = "";
    int ÑurrentProblemIndex; // Not Used

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
        //CanvasUI.SetActive(false);
        if (locationButton != null)
        {

            locationButton.onClick.AddListener(CloseHallMiniGame);
            //Debug.LogError("AddListener");
        }
        else
        {
            Debug.LogError("Button with name  not found!");
        }
        

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
        ÑurrentProblem = randomEquation.Key;
        equationText.text = ÑurrentProblem;
        currentAnswer = "";
        answerText.text = currentAnswer;
    }

    void Update()
    {

    }
    public void CloseHallMiniGame()
    {
       // Debug.LogError("Button with name!");


        //  SoundManager.Instance.PlaySound(SoundManager.SoundClip.DoorCreak);
        SceneManager.LoadScene(LocationSceneName);
        //CanvasUI.SetActive(true);

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
            int correctAnswer = Problems[ÑurrentProblem]; //áåðåì ïðàâèëüíûé îòâåò èç ñëîâàðÿ ïî êëþ÷ó
            Problems.Remove(ÑurrentProblem);
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
            
            CloseHallMiniGame();
        }
       
    }
}
