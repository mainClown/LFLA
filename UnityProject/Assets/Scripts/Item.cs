using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    /* (����� ��)
     * ��� ������ ������� �� ����� ����� ���������� ������ EventSystem(��������� �� ������� UI)
     * � ������ ���������� ��������� Physics 2D Raycaster
     */

    public string Name;
    public int ItemId;
    //public bool Necessary;
    public Sprite InventorySprite;
    public Sprite HighlightSprite; // ������������ ��������
    public Sprite NoHighlightSprite; // ������� ��������
    private Image ImageObject;

    // Start is called before the first frame update
    void Start()
    {
        ImageObject = GetComponent<Image>();
        // ��������� �������� ��������
        if (ImageObject.sprite == null)
        {
            ImageObject.sprite = NoHighlightSprite;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //HighlightItem();
        ImageObject.sprite = HighlightSprite;
        NewDoor.ShowDisplay(Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //UnhighlightItem();
        ImageObject.sprite = NoHighlightSprite;
        NewDoor.HideDisplay();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //HideItem();
        SoundManager.Instance.PlaySound(SoundManager.SoundClip.ItemPick);
        gameObject.SetActive(false);
        Inventory.Instance.AddItem(this);
        NewDoor.HideDisplay();
    }
    // �������� ������� � �������� AddItem(int ItemId) ��� ���������� ����� �������� � ���������.
    private void HideItem()
    {
    }

    // ������ �������� �������� �� ������������ ������� ��� ��������� �� ���� ������.
    private void HighlightItem()
    {
    }
}
