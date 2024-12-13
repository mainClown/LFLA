using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class KitchenMiniGame : MonoBehaviour
{
    // Game Objects and Components
    public GameObject plate;
    public Rigidbody2D rb_plate;
    public GameObject[] FallingItems; // Префабы продуктов !
    public Button CloseButton;
    public Sprite InventorySprite;
    // Plate Movement
    public float smoothSpeed = 50000f;
    public float maxX;
    public float minX;
    public float width = 437f; 

    // Product Spawning
    public float spawnRate = 3f;
    private bool isSpawning = false;
    public float minFallSpeed = 1.5f;
    public float maxFallSpeed = 15f;

    // Score Management
    private int CurrentItems = 0;
    int ItemsToWin = 15;
    void Start()
    {
        Camera mainCamera = Camera.main;
        Inventory.Instance.GetComponent<Canvas>().worldCamera = mainCamera;
        CloseButton.onClick.AddListener(CloseKitchenMiniGame);
        Timer.Instance.OnMiniGameEnd += CloseKitchenMiniGame;

        plate = GameObject.Find("Plate");
        if (plate == null)
        {
            Debug.LogError("Plate GameObject not found!");
            return;
        }
        rb_plate = plate.GetComponent<Rigidbody2D>();
        if (rb_plate == null)
        {
            Debug.LogError("Rigidbody2D component not found on Plate!");
            return;
        }
        BoxCollider2D boxCollider = plate.GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            width = boxCollider.size.x;
        }
        else
        {
            Debug.LogError("Plate GameObject does not have a BoxCollider2D!");
            return;
        }

        float cameraOrthographicSize = mainCamera.orthographicSize;
        float cameraAspect = mainCamera.aspect;
        maxX = cameraAspect * cameraOrthographicSize - (width * 2.5f * cameraAspect);
        minX = -maxX;
        StartCoroutine(SpawnProductsCoroutine());
    }


    void Update()
    {
        MovePlate();
    }


    void MovePlate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        float targetX = Mathf.Clamp(mousePosition.x, minX, maxX);
        Vector2 targetPosition = new Vector2(targetX, plate.transform.position.y);
        rb_plate.MovePosition(Vector2.Lerp(rb_plate.position, targetPosition, smoothSpeed * Time.deltaTime));
    }

    IEnumerator SpawnProductsCoroutine()
    {
        while (true)
        {
            if (!isSpawning)
            {
                isSpawning = true;
                SpawnItem();
                yield return new WaitForSeconds(spawnRate);
                isSpawning = false;
            }
            yield return null; // Убедитесь, что корутина не блокирует Update()
        }
    }

    void SpawnItem()
    {
        if (FallingItems.Length == 0)
        {
            Debug.LogError("No product prefabs assigned!");
            return; 
        }
        int randomIndex = Random.Range(0, FallingItems.Length);
        GameObject productPrefab = FallingItems[randomIndex];
        float randomX = Random.Range(minX, maxX);
        float startY = 55f; // Spawn products above the screen
        Vector3 spawnPosition = new Vector3(randomX, startY, 0);

        GameObject newProduct = Instantiate(productPrefab, spawnPosition, Quaternion.identity);
        if (newProduct != null)
        {
            ProductCollision productCollision = newProduct.AddComponent<ProductCollision>(); 
            productCollision.scoreManage = this; 

            if (Camera.main != null)
            {
                float destroyY = Camera.main.transform.position.y - Camera.main.orthographicSize - 1f;
                productCollision.fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
                productCollision.destroyY = destroyY;
            }
        }
    }




    public void IncreaseScore()
    {
        CurrentItems++;
        CheckWin();
    }

    public void DecreaseScore()
    {
        CurrentItems--;
    }


    void CheckWin()
    {
        if (CurrentItems >= ItemsToWin)
        {
            GameObject itemObject = new GameObject("Breakfast");
            Item item = itemObject.AddComponent<Item>();

            item.ItemId = 23;
            item.Name = "Завтрак";
            item.InventorySprite = InventorySprite;

            // Добавление в инвентарь
            Inventory.Instance.AddItem(item);
            CloseKitchenMiniGame();
        }
    }
    public void CloseKitchenMiniGame()
    {
        Timer.Instance.ResetMiniGameTimer();
        SceneManager.LoadScene("KitchenScene");
    }
}


//ProductCollision Script
public class ProductCollision : MonoBehaviour
{
    public KitchenMiniGame scoreManage;
    public float destroyY;
    public float fallSpeed;
    private bool isFalling = true;



    void Update()
    {

        if (isFalling)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime); // Added Time.deltaTime to control speed
            if (transform.position.y < destroyY)
            {
                if (scoreManage != null)
                {
                    scoreManage.DecreaseScore();
                }
                Destroy(gameObject);
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plate"))
        {
            if (scoreManage != null)
            {
                scoreManage.IncreaseScore();
            }
            Destroy(gameObject);
        }
    }
}
