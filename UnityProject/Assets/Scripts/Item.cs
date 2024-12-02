using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    /* (����� ��)
     * ��� ������ ������� �� ����� ����� ���������� ������ EventSystem(��������� �� ������� UI)
     * � ������ ���������� ��������� Physics 2D Raycaster
     */
    public string Name;
    public int ItemId;
    public bool Necessary;
    public Sprite HighliteFileName; // ������������ ��������
    public Sprite NoHighliteFileName; // ������� ��������
    private SpriteRenderer spriteRenderer;
    public Text nameDisplay; // ������ �� UI ����� ��� ����������� �����

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // ��������� �������� ��������
        if(spriteRenderer == null)
        {
            spriteRenderer.sprite = NoHighlightSprite;
        }   
    }

    private void OnMouseEnter()
    {
        HighliteItem(ItemId);
        nameDisplay.text = Name; // ��������� ������
        nameDisplay.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = NoHighlightSprite;
        nameDisplay.gameObject.SetActive(false); // ������� ������
    }

    // Update is called once per frame
    void Update()
    {

    }

    // �������� ������� � �������� AddItem(int ItemId) ��� ���������� ����� �������� � ���������.
    void HideItem(int ItemId)
    {
        GameObject.SetActive(false);
        Inventory.AddItem(ItemId);
    }

    // ������ �������� �������� �� ������������ ������� ��� ��������� �� ���� ������.
    void HighliteItem(int ItemId)
    {
        spriteRenderer.sprite = HighlightSprite;
    }
}
