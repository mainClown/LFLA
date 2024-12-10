using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NKM : MonoBehaviour
{
    public float smoothSpeed = 50000f; // �������� ����������� �������� ������� (�������� ��������)
    public float maxX; // ������������ ��������� �� ��� X
    public float minX; // ����������� ��������� �� ��� X
    public float width = 437;
    public float height = 0;
    public Rigidbody2D rb_plate;
    public GameObject plate;
    public string kitchenSceneName = "KitchenScene";
    public int score = 0;
    public GameObject[] itemPrefabs; // ������ �������� ���������
    public float spawnRate = 1f; // ������� ��������� ��������� (� ��������)
    public float minXi, maxXi; // ����������� � ������������ �������� X ��� ������
    public float startY; // Y-���������� ��� ������ (������ ������)
    public float spawnHeightAboveScreen = 2f; // ������ ��� ������� ��� ������
    public float horizontalPadding = 1f; // ������ �� ����� ������
    public float fallSpeed = 5f; // �������� �������
    public float destroyY = -50f; // Y-���������� ��� �����������
    private bool isFalling = true;
   

    private Camera mainCamera;

    void Start()
    {
        
        plate = GameObject.Find("Plate");
        if (plate == null)
        {
            Debug.LogError("Plate GameObject not found!");
            return; // ����� �� �������, ���� ������ �� ������
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
            return; // ����� �� �������, ���� ��������� �� ������
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
        float cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect; //��� ��������������� ������
        minXi = -cameraHalfWidth + horizontalPadding;
        maxXi = cameraHalfWidth - horizontalPadding;
        startY = mainCamera.transform.position.y + mainCamera.orthographicSize + spawnHeightAboveScreen; //��� ��������������� ������

        StartCoroutine(SpawnItems());
    }

    void Update()
    {
        MovePlate();
        if (isFalling && this.gameObject.name != "Plate")
        {
            // ������� ��������
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
        // float destroyY = mainCamera.transform.position.y - mainCamera.orthographicSize - 1f;
        // ����������� ��� ������ �� ������ ���� ������

        if (transform.position.y < destroyY && this.gameObject.name != "Plate")
        {
            
                DecreaseScore();
            
            Destroy(gameObject);
        }
        
    }

    void MovePlate()
    {
        // �������� ������� ���� � ������� �����������
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        // ��������� ������� ������� �������
        float targetX = Mathf.Clamp(mousePosition.x, minX, maxX);

        // ���������� ������� � ������� Lerp (����������)
        Vector2 targetPosition = new Vector2(targetX, plate.transform.position.y);
        rb_plate.MovePosition(Vector2.Lerp(rb_plate.position, targetPosition, smoothSpeed)); // ���������� rb_plate.position

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ��� ������������ ��������� � ��������� (��������, �� ����)
        if (collision.gameObject.CompareTag("Product"))
        {
           
              IncreaseScore();
          
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Plate"))
        {

            Destroy(gameObject);
            //isFalling = false; //���������� �������
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

            // �������� ����� "Kitchen"
         //   LoadScene(kitchenSceneName);
        }
    }
    
    IEnumerator SpawnItems()
    {
        while (true)
        {
            // ���� �������� ���������� �������
            yield return new WaitForSeconds(spawnRate);

            // ���������� ��������� ��� ��������
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            GameObject itemPrefab = itemPrefabs[randomIndex];

            // ���������� ��������� ���������� X
            float randomX = Random.Range(minXi, maxXi);

            // ������� ��������� ��������
            GameObject newItem = Instantiate(itemPrefab, new Vector3(randomX, startY, 0), Quaternion.identity);
            //ItemController itemController = newItem.AddComponent<MiniGameItem>();
           
            // ��������� ������ ��� ���������� �������� � ������������
            //  newItem.AddComponent<ItemController>();
           
                
          
        }
    }


}

