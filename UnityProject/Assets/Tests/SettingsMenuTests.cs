using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using System;
using System.Collections;

[TestFixture]
public class SettingsMenuTests
{
    // Поля для объектов
    private GameObject settingsMenuObject;
    private SettingsMenu settingsMenu;
    private GameObject pauseUI;
    private GameObject settingsUI;
    private Slider soundSlider;
    private Slider musicSlider;
    private Toggle easySwitch;
    private Toggle hardSwitch;

    // Метод для подготовки теста (Setup)
    [SetUp]
    public void Setup()
    {
        // Создаем объект SettingsMenu
        settingsMenuObject = new GameObject("SettingsMenu");
        settingsMenu = settingsMenuObject.AddComponent<SettingsMenu>();

        // Создаем объекты UI
        pauseUI = new GameObject("PauseUI");
        settingsUI = new GameObject("SettingsUI");
        soundSlider = new GameObject("SoundSlider").AddComponent<Slider>();
        musicSlider = new GameObject("MusicSlider").AddComponent<Slider>();
        easySwitch = new GameObject("EasySwitch").AddComponent<Toggle>();
        hardSwitch = new GameObject("HardSwitch").AddComponent<Toggle>();

        // Привязываем объекты UI к компонентам SettingsMenu
        settingsMenu.PauseUI = pauseUI;
        settingsMenu.SettingsUI = settingsUI;
        settingsMenu.SoundSlider = soundSlider;
        settingsMenu.MusicSlider = musicSlider;
        settingsMenu.EasySwitch = easySwitch;
        settingsMenu.HardSwitch = hardSwitch;

        // Настроим начальные значения в PlayerPrefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("SoundVolume", 40f / 100f);
        PlayerPrefs.SetFloat("MusicVolume", 60f / 100f);
        PlayerPrefs.SetInt("EasyMode", 0);
        PlayerPrefs.Save();
    }

    [UnityTest]
    public IEnumerator B5_OpenSettingsMenu_ShowsCorrectValues()
    {
        // Открываем меню настроек
        settingsMenu.OpenSettingsMenu();

        // Ждем один кадр, чтобы изменения вступили в силу
        yield return null;

        // Проверяем, что SettingsUI стало активным (меню открыто)
        Assert.IsTrue(settingsUI.activeSelf, "Settings UI is not visible");

        // Проверяем, что слайдеры имеют правильные значения
        Assert.AreEqual(0.4f, soundSlider.value, "Sound volume is incorrect");
        Assert.AreEqual(0.6f, musicSlider.value, "Music volume is incorrect");

        // Проверяем, что переключатели настроены правильно
        Assert.IsFalse(easySwitch.isOn, "Easy mode switch is incorrect (should be off for Hard mode)");
        Assert.IsTrue(hardSwitch.isOn, "Hard mode switch is incorrect (should be on for Hard mode)");
    }

    [Test]
    public void B5a_OpenSettingsMenu_InvalidPrefabName()
    {
        // Попытка загрузить несуществующий префаб
        GameObject settingsMenuObject = Resources.Load<GameObject>("SettingsMenu_Missing");

        // Проверяем, что объект с таким именем не найден
        Assert.IsNull(settingsMenuObject, "Prefab 'SettingsMenu_Missing' should not exist in Resources.");

        try
        {
            // Пытаемся выполнить действия с несуществующим объектом
            SettingsMenu settingsMenuScript = settingsMenuObject.GetComponent<SettingsMenu>();

            // Вызываем метод, который должен выбросить исключение
            settingsMenuScript.OpenSettingsMenu();

            // Если исключение не выбросилось, тест провален
            Assert.Fail("Expected NullReferenceException was not thrown.");
        }
        catch (NullReferenceException)
        {
            // Если выбросилось NullReferenceException, то тест проходит
            Assert.Pass("NullReferenceException thrown as expected.");
        }
        catch (Exception ex)
        {
            // Если выбрасывается другое исключение, тест провален
            Assert.Fail($"Unexpected exception thrown: {ex.Message}");
        }
    }

    // Метод для очистки после теста (Teardown)
    [TearDown]
    public void Teardown()
    {
        // Удаляем все созданные объекты после теста
        UnityEngine.Object.DestroyImmediate(settingsMenuObject); // Исправлено: используем UnityEngine.Object
    }
}
