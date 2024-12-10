using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NKM : MonoBehaviour
{
    public float smoothSpeed = 50000f; // Скорость сглаживания движения коробки (изменено название)
    public float maxX; // Максимальное положение по оси X
    public float minX; // Минимальное положение по оси X
    public float width = 437;
    public float height = 0;
    public Rigidbody2D rb_plate;
    public GameObject plate;
    public string kitchenSceneName = "KitchenScene";
    public int score = 0;
    public GameObject[] itemPrefabs; // Массив префабов предметов
    public float spawnRate = 1f; // Частота появления предметов (в секундах)
    public float minXi, maxXi; // Минимальное и максимальное значение X для спавна
    public float startY; // Y-координата для спавна (сверху экрана)
    public float spawnHeightAboveScreen = 2f; // Высота над экраном для спавна
    public float horizontalPadding = 1f; // Отступ от краев экрана
    public float fallSpeed = 5f; // Скорость падения
    public float destroyY = -50f; // Y-координата для уничтожения
    private bool isFalling = true;
   

    private Camera mainCamera;

    void Start()
    {
        
        plate = GameObject.Find("Plate");
        if (plate == null)
        {
            Debug.LogError("Plate GameObject not found!");
            return; // Выход из функции, если объект не найден
        }
        rb_plate = plate.GetComponent<Rigidbody2D>();
        Collider2D collider = plate.GetComponent<Collider2D>();
        if (collider != null)
        {
            if (collider is BoxCollider2D boxCollider)
            {
                width = boxCollider.size.x;
            }
        }
        if (rb_plate == null)
        {
            Debug.LogError("Rigidbody2D component not found on Plate!");
            return; // Выход из функции, если компонент не найден
        }
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            float cameraOrthographicSize = mainCamera.orthographicSize;
            float cameraAspect = mainCamera.aspect;
            maxX = cameraAspect * cameraOrthographicSize - (width * 2.5f * cameraAspect);
            minX = -maxX;
        }
        else
        {
            Debug.LogError("Main camera not found!");
        }
        float cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect; //Для ортографической камеры
        minXi = -cameraHalfWidth + horizontalPadding;
        maxXi = cameraHalfWidth - horizontalPadding;
        startY = mainCamera.transform.position.y + mainCamera.orthographicSize + spawnHeightAboveScreen; //Для ортографической камеры

        StartCoroutine(SpawnItems());
    }

    void Update()
    {
        MovePlate();
        if (isFalling && this.gameObject.name != "Plate")
        {
            // Падение предмета
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
        // float destroyY = mainCamera.transform.position.y - mainCamera.orthographicSize - 1f;
        // Уничтожение при выходе за нижний край экрана

        if (transform.position.y < destroyY && this.gameObject.name != "Plate")
        {
            
                DecreaseScore();
            
            Destroy(gameObject);
        }
        
    }

    void MovePlate()
    {
        // Получаем позицию мыши в мировых координатах
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        // Вычисляем целевую позицию коробки
        float targetX = Mathf.Clamp(mousePosition.x, minX, maxX);

        // Перемещаем коробку с помощью Lerp (исправлено)
        Vector2 targetPosition = new Vector2(targetX, plate.transform.position.y);
        rb_plate.MovePosition(Vector2.Lerp(rb_plate.position, targetPosition, smoothSpeed)); // Используем rb_plate.position

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, что столкновение произошло с продуктом (например, по тегу)
        if (collision.gameObject.CompareTag("Product"))
        {
           
              IncreaseScore();
          
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Plate"))
        {

            Destroy(gameObject);
            //isFalling = false; //Остановить падение
        }
    }
    public void IncreaseScore()
    {
        score++;
        Debug.LogError(score);
        CheckForWin();
    }

    public void DecreaseScore()
    {
        score--;
        Debug.LogError(score);
    }
    void CheckForWin()
    {
        if (score >= 10)
        {
            Debug.LogError(kitchenSceneName);

            // Загрузка сцены "Kitchen"
         //   LoadScene(kitchenSceneName);
        }
    }
    
    IEnumerator SpawnItems()
    {
        while (true)
        {
            // Ждем заданный промежуток времени
            yield return new WaitForSeconds(spawnRate);

            // Генерируем случайный тип предмета
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            GameObject itemPrefab = itemPrefabs[randomIndex];

            // Генерируем случайную координату X
            float randomX = Random.Range(minXi, maxXi);

            // Создаем экземпляр предмета
            GameObject newItem = Instantiate(itemPrefab, new Vector3(randomX, startY, 0), Quaternion.identity);
            //ItemController itemController = newItem.AddComponent<MiniGameItem>();
           
            // Добавляем скрипт для управления падением и уничтожением
            //  newItem.AddComponent<ItemController>();
           
                
          
        }
    }


}

