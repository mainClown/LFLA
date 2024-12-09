using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryIntegrationTest
{
    [Test]
    public void TestAddItemToInventory()
    {
        // Arrange
        // Создаем тестовые данные
        Item item1 = new GameObject().AddComponent<Item>();
        item1.Name = "Clothes";
        item1.ItemId = 1;
        item1.InventorySprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);
        item1.HighlightSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);
        item1.NoHighlightSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);

        Item item2 = new GameObject().AddComponent<Item>();
        item2.Name = "Phone";
        item2.ItemId = 2;
        item2.InventorySprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);
        item2.HighlightSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);
        item2.NoHighlightSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);

        Item item3 = new GameObject().AddComponent<Item>();
        item3.Name = "usb";
        item3.ItemId = 3;
        item3.InventorySprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);
        item3.HighlightSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);
        item3.NoHighlightSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);

        Inventory inventory = new GameObject().AddComponent<Inventory>();
        inventory.InventoryUI = new GameObject();
        inventory.CellContainerObject = new GameObject();
        inventory.CanvasUI = new GameObject();
        inventory.ItemCounter = new GameObject();

        // Добавляем компоненты, необходимые для работы Inventory
        inventory.InventoryUI.AddComponent<Image>();
        inventory.CellContainerObject.AddComponent<Image>();
        inventory.ItemCounter.AddComponent<TextMeshProUGUI>();

        // Добавляем дочерний объект в CellContainerObject
        GameObject cellObject = new GameObject("Cell");
        cellObject.transform.SetParent(inventory.CellContainerObject.transform);
        cellObject.AddComponent<Image>();

        // Act
        // Добавляем предметы в инвентарь
        inventory.AddItem(item1);
        inventory.AddItem(item2);
        inventory.AddItem(item3);

        // Assert
        // Проверяем, что предметы добавлены в инвентарь
        Assert.AreEqual(3, Inventory.SelectedItems.Count);
        Assert.IsTrue(Inventory.SelectedItems.Contains(item1));
        Assert.IsTrue(Inventory.SelectedItems.Contains(item2));
        Assert.IsTrue(Inventory.SelectedItems.Contains(item3));
    }
}