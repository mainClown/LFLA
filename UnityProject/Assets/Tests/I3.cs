using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

[TestFixture]
public class InventoryTests
{
    private GameObject inventoryObject;
    private Inventory inventory;

    [SetUp]
    public void Setup()
    {
        // Создаем объект инвентаря
        inventoryObject = new GameObject("Inventory");
        inventory = inventoryObject.AddComponent<Inventory>();

        // Устанавливаем синглтон вручную для тестов
        inventory.SetInstanceForTests();  // Временная замена синглтона

        // Создаем временные заглушки для UI объектов, которые могут быть null
        inventory.InventoryUI = new GameObject("InventoryUI");
        inventory.CellContainerObject = new GameObject("CellContainerObject");

        // Создаем объект ItemCounter и добавляем к нему компонент TextMeshProUGUI
        inventory.ItemCounter = new GameObject("ItemCounter");
        inventory.ItemCounter.AddComponent<TextMeshProUGUI>();

        // Заглушка для CanvasUI
        inventory.CanvasUI = new GameObject("CanvasUI");

        // Убедимся, что список SelectedItems пустой перед каждым тестом
        Inventory.SelectedItems.Clear();
    }

    [Test]
    public void AddItem_AddsItemToInventory()
    {
        // Подготовка тестовых данных
        var item1 = new GameObject("Clothes").AddComponent<Item>();
        item1.Name = "Clothes";
        item1.ItemId = 1;
        
        var item2 = new GameObject("Phone").AddComponent<Item>();
        item2.Name = "Phone";
        item2.ItemId = 2;

        var item3 = new GameObject("usb").AddComponent<Item>();
        item3.Name = "usb";
        item3.ItemId = 3;

        // Имитируем добавление предметов в инвентарь
        Inventory.Instance.AddItem(item1);
        Inventory.Instance.AddItem(item2);

        // Проверяем, что предметы добавлены в список
        Assert.AreEqual(Inventory.SelectedItems.Count, 2);

        // Добавляем новый предмет
        Inventory.Instance.AddItem(item3);

        // Проверяем, что предмет добавился в инвентарь
        Assert.AreEqual(Inventory.SelectedItems.Count, 3);
        Assert.AreEqual(Inventory.SelectedItems[2], item3);
    }

    [TearDown]
    public void TearDown()
    {
        // Очищаем ресурсы после теста
        Object.DestroyImmediate(inventoryObject);
    }
}
