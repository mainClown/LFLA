using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.CompilerServices;

public class BathroomMinigame : MonoBehaviour
{
    // Game Objects and Components
    public Button CloseButton;
   // public GameObject CanvasUI;
    public GameObject cursor;
    public GameObject pauseBtn;
    public GameObject inventoryBtn;
    public GameObject line;
    public GameObject left;
    public GameObject right;
    public SpriteRenderer lineRenderer;
    public Sprite InventorySprite;
    public GameObject plumb;


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
    public float Speed = 5f;
    private float initialScreenX;

    public static bool playerWon = false;

    private Vector3 plumbInitialPosition;

    private void Awake()
    {
        pauseBtn = (GameObject.Find("PauseBtn"));
        inventoryBtn = (GameObject.Find("InventoryBtn"));
        (pauseBtn).SetActive(false);
        (inventoryBtn).SetActive(false);
        Vector3 initialScreenPos = Camera.main.WorldToScreenPoint(cursor.transform.position);
        Vector3 initialleft = Camera.main.WorldToScreenPoint(left.transform.position);
        Vector3 initialright = Camera.main.WorldToScreenPoint(right.transform.position);
        initialScreenX = initialScreenPos.x;
        greenSectorStart = initialleft.x;
        greenSectorEnd = initialright.x;
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
        if (plumb == null)
        {
            Debug.LogError("GameObject 'plumb' is not assigned in the Inspector!");
        }

        // Запоминаем начальную позицию
        plumbInitialPosition = plumb.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        MoveСursor();
        // Score update on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            CheckHit();
            StartCoroutine(MoveDownAndUp());
        }
    }
    IEnumerator MoveDownAndUp()
    {
        // Двигаем вниз
        Vector3 targetPosition = plumb.transform.position + new Vector3(0, -10, 0); // -50 по оси Y
        float elapsedTime = 0;
        float moveDuration = 0.2f; // Время движения вниз (в секундах)

        while (elapsedTime < moveDuration)
        {
            plumb.transform.position = Vector3.Lerp(plumb.transform.position, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Возвращаем на место
        elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            plumb.transform.position = Vector3.Lerp(plumb.transform.position, plumbInitialPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
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
       // Debug.Log(screenPos.x + "sc_pos" + greenSectorStart + "start" + greenSectorEnd + "end");
        if (screenPos.x >= greenSectorStart && screenPos.x <= greenSectorEnd)
        {
            CurrentHits++;
            CheckWin();

            // Debug.Log("Score: " + CurrentHits + " (Green)");
        }
        else
        {
            CurrentHits--;
            //Debug.Log("Score: " + CurrentHits + " (Red)");
        }
        //Debug.Log(CurrentHits);
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
            playerWon = true;
            CloseBathroomMiniGame();
        }
    }
    public void CloseBathroomMiniGame()
    {
        Timer.Instance.ResetMiniGameTimer();
        TextBubble.Instance.StartAgain();
        (pauseBtn).SetActive(true);
        (inventoryBtn).SetActive(true);
        SceneManager.LoadScene("BathroomScene"); 
        
    }
}
