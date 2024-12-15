using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.CompilerServices;

public class BathroomMinigame : MonoBehaviour
{
    // Game Objects and Components
    public Button CloseButton;
    public GameObject CanvasUI;
    public GameObject cursor;
    public GameObject line;
    public SpriteRenderer lineRenderer;
    public Sprite InventorySprite;

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
        Camera mainCamera = Camera.main;
        Inventory.Instance.GetComponent<Canvas>().worldCamera = mainCamera;
        CloseButton.onClick.AddListener(CloseBathroomMiniGame);
        Timer.Instance.OnMiniGameEnd += CloseBathroomMiniGame;
        TextBubble.Instance.HideTextBubble();
        lineLength = lineRenderer.bounds.size.x;
        //Initial Cursor Position
         cursorPosition = line.transform.position.x - lineLength / 2f;
        cursor.transform.position = new Vector3(cursorPosition, cursor.transform.position.y, cursor.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        MoveСursor();
        // Score update on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            CheckHit();
        }
    }

    void MoveСursor()
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
        Debug.Log(CurrentHits);
    }

    void CheckWin()
    {
        if (CurrentHits >= hitsToWin)
        {
            GameObject itemObject = new GameObject("BikeKeys");
            Item item = itemObject.AddComponent<Item>();

            item.ItemId = 16;
            item.Name = "Ключи от велосипеда";
            item.InventorySprite = InventorySprite;

            // Добавление в инвентарь
            Inventory.Instance.AddItem(item);
            CloseBathroomMiniGame();
        }
    }
    public void CloseBathroomMiniGame()
    {
        Timer.Instance.ResetMiniGameTimer();
        TextBubble.Instance.StartAgain();
        SceneManager.LoadScene("BathroomScene");
    }
}
