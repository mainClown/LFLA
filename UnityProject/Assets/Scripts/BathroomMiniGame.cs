using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class BathroomMinigame : MonoBehaviour
{
    // Game Objects and Components
    public Button locationButton;
    public string LocationSceneName;
    public GameObject CanvasUI;
    public GameObject cursor;
    public GameObject line;
    public SpriteRenderer lineRenderer;

    //Line
    private float lineLength;
    private float greenSectorStart;
    private float greenSectorEnd;

    //Score Management
    private int CurrentHits = 0;
    int hitsToWin = 5;
    
    //Coursor
    private bool movingRight = true;
    private float cursorPosition;
    public float Speed = 50f;
    private float initialScreenX;

    private void Awake()
    {
        Vector3 initialScreenPos = Camera.main.WorldToScreenPoint(cursor.transform.position);
        initialScreenX = initialScreenPos.x;
        greenSectorStart = initialScreenX + 277f;
        greenSectorEnd = greenSectorStart + 316f;
    }
    void Start()

    {
        //CanvasUI.SetActive(false);
        if (locationButton != null)
        {

            locationButton.onClick.AddListener(CloseBathroomMiniGame);
           // Debug.LogError("AddListener");
        }
        else
        {
            Debug.LogError("Button locationButton not found!");
        }
        lineLength = lineRenderer.bounds.size.x;
       // Debug.LogError(greenSectorStart+"Start");
        //Debug.LogError(greenSectorEnd + "End");

        //Initial Cursor Position
         cursorPosition = line.transform.position.x - lineLength / 2f;
        cursor.transform.position = new Vector3(cursorPosition, cursor.transform.position.y, cursor.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        MoveÑursor();
        // Score update on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            CheckHit();
        }
    }

    void MoveÑursor()
    {
if (movingRight)
        {
            cursorPosition += Speed * Time.deltaTime;
            if (cursorPosition >= line.transform.position.x + lineLength / 2f)
            {
                movingRight = false;
            }
        }
        else
        {
            cursorPosition -= Speed * Time.deltaTime;
            if (cursorPosition <= line.transform.position.x - lineLength / 2f)
            {
                movingRight = true;
            }
        }
        cursor.transform.position = new Vector3(cursorPosition, cursor.transform.position.y, cursor.transform.position.z);

    }
    void CheckHit()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(cursor.transform.position);

        if (screenPos.x >= greenSectorStart && screenPos.x <= greenSectorEnd)
        {
            CurrentHits++;
            CheckWin();

            // Debug.Log("Score: " + score + " (Green)");
        }
        else
        {
            CurrentHits--;
            //Debug.Log("Score: " + score + " (Red)");
        }
    }

    void CheckWin()
    {
        if (CurrentHits >= hitsToWin)
        {
            CloseBathroomMiniGame();
        }
    }
    public void CloseBathroomMiniGame()
    {
       // Debug.LogError("Button with name!");


        //  SoundManager.Instance.PlaySound(SoundManager.SoundClip.DoorCreak);
        SceneManager.LoadScene(LocationSceneName);
        //CanvasUI.SetActive(true);

    }
}
