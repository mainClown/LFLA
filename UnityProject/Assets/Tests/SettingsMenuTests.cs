using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Reflection;

[TestFixture]
public class SettingsMenuTests
{
    // Переменные для компонентов
    private GameObject settingsMenuObject;
    private SettingsMenu settingsMenu;
    private GameObject pauseUI;
    private GameObject settingsUI;
    private Slider soundSlider;
    private Slider musicSlider;
    private Toggle easySwitch;
    private Toggle hardSwitch;

    // Настройка перед каждым тестом (SetUp)
    [SetUp]
    public void Setup()
    {
        // Создаем новый объект SettingsMenu
        settingsMenuObject = new GameObject("SettingsMenu");
        settingsMenu = settingsMenuObject.AddComponent<SettingsMenu>();

        // Создаем UI элементы
        pauseUI = new GameObject("PauseUI");
        settingsUI = new GameObject("SettingsUI");
        soundSlider = new GameObject("SoundSlider").AddComponent<Slider>();
        musicSlider = new GameObject("MusicSlider").AddComponent<Slider>();
        easySwitch = new GameObject("EasySwitch").AddComponent<Toggle>();
        hardSwitch = new GameObject("HardSwitch").AddComponent<Toggle>();

        // Привязываем UI элементы к SettingsMenu
        settingsMenu.PauseUI = pauseUI;
        settingsMenu.SettingsUI = settingsUI;
        settingsMenu.SoundSlider = soundSlider;
        settingsMenu.MusicSlider = musicSlider;
        settingsMenu.EasySwitch = easySwitch;
        settingsMenu.HardSwitch = hardSwitch;

        // Настроим начальные значения в PlayerPrefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("SoundVolume", 40f / 100f); // 40%
        PlayerPrefs.SetFloat("MusicVolume", 60f / 100f); // 60%
        PlayerPrefs.SetInt("EasyMode", 0); // Hard mode (0)
        PlayerPrefs.Save();
    }

    // Тест для открытия меню настроек (положительный)
    [UnityTest]
    public IEnumerator B5_OpenSettingsMenu_ShowsCorrectValues()
    {
        // Открываем меню настроек
        settingsMenu.OpenSettingsMenu();

        // Ждем 1 кадр, чтобы UI обновился
        yield return null;

        // Проверяем, что SettingsUI активен
        Assert.IsTrue(settingsUI.activeSelf, "Settings UI is not visible");

        // Проверяем, что значения слайдеров правильные
        Assert.AreEqual(0.4f, soundSlider.value, "Sound volume is incorrect");
        Assert.AreEqual(0.6f, musicSlider.value, "Music volume is incorrect");

        // Проверяем, что переключатели правильны для режима сложности
        Assert.IsFalse(easySwitch.isOn, "Easy mode switch is incorrect (should be off for Hard mode)");
        Assert.IsTrue(hardSwitch.isOn, "Hard mode switch is incorrect (should be on for Hard mode)");
    }

    // Тест на случай, если отсутствует Prefab (проверка на NullReferenceException)
    [Test]
    public void B5a_OpenSettingsMenu_InvalidPrefabName()
    {
        // Попробуем загрузить несуществующий prefab
        GameObject settingsMenuObject = Resources.Load<GameObject>("SettingsMenu_Missing");

        // Проверяем, что такой prefab не существует
        Assert.IsNull(settingsMenuObject, "Prefab 'SettingsMenu_Missing' should not exist in Resources.");

        try
        {
            // Пытаемся получить компонент и вызвать метод, что должно привести к ошибке
            SettingsMenu settingsMenuScript = settingsMenuObject.GetComponent<SettingsMenu>();
            settingsMenuScript.OpenSettingsMenu();

            // Если ошибки не будет, тест не пройден
            Assert.Fail("Expected NullReferenceException was not thrown.");
        }
        catch (NullReferenceException)
        {
            // Если возникла ошибка, тест пройден
            Assert.Pass("NullReferenceException thrown as expected.");
        }
        catch (Exception ex)
        {
            // Если ошибка не NullReferenceException, тест не пройден
            Assert.Fail($"Unexpected exception thrown: {ex.Message}");
        }
    }

    // Тест для загрузки настроек (проверка корректности загрузки)
    [UnityTest]
    public IEnumerator B6_LoadSettingsAfterSave_LoadsCorrectValues()
    {
        // Сначала сохраняем настройки в PlayerPrefs
        PlayerPrefs.SetFloat("SoundVolume", 0.7f);
        PlayerPrefs.SetFloat("MusicVolume", 0.9f);
        PlayerPrefs.SetInt("EasyMode", 1); // Easy mode
        PlayerPrefs.Save();

        // Открываем меню настроек
        settingsMenu.OpenSettingsMenu();

        // Ждем 1 кадр, чтобы UI обновился
        yield return null;

        // Проверяем, что значения слайдеров правильные после загрузки из PlayerPrefs
        Assert.AreEqual(0.7f, soundSlider.value, "Sound volume is not loaded correctly");
        Assert.AreEqual(0.9f, musicSlider.value, "Music volume is not loaded correctly");

        // Проверяем, что переключатели для сложности верны
        Assert.IsTrue(easySwitch.isOn, "Easy mode switch is incorrect (should be on for Easy mode)");
        Assert.IsFalse(hardSwitch.isOn, "Hard mode switch is incorrect (should be off for Easy mode)");
    }


    [UnityTest]
    public IEnumerator B6a_SaveSettings_WithInvalidValues_DoesNotSaveInvalidData()
    {
        // Проверяем, что все компоненты UI были инициализированы и не равны null
        Assert.IsNotNull(soundSlider, "SoundSlider is not assigned");
        Assert.IsNotNull(musicSlider, "MusicSlider is not assigned");
        Assert.IsNotNull(easySwitch, "EasySwitch is not assigned");
        Assert.IsNotNull(hardSwitch, "HardSwitch is not assigned");

        // Устанавливаем некорректные значения для громкости звука и музыки
        soundSlider.value = -0.1f; // Неверное значение (-10%)
        musicSlider.value = 1.5f;  // Неверное значение (150%)
        easySwitch.isOn = true;    // Easy mode

        // Инициализируем объекты SoundManager и MusicManager
        var soundManagerMock = new GameObject("SoundManagerMock").AddComponent<SoundManager>();
        var musicManagerMock = new GameObject("MusicManagerMock").AddComponent<MusicManager>();

        // Активируем объекты
        soundManagerMock.gameObject.SetActive(true);
        musicManagerMock.gameObject.SetActive(true);

        // Используем рефлексию, чтобы заменить синглтоны
        SetSingletonInstance<SoundManager>("Instance", soundManagerMock);
        SetSingletonInstance<MusicManager>("Instance", musicManagerMock);

        // Сохраняем настройки
        settingsMenu.SaveSettings();

        // Проверяем, что значения не выходят за пределы диапазона 0-1
        Assert.AreEqual(0f, PlayerPrefs.GetFloat("SoundVolume"), "Sound volume should be clamped to 0");
        Assert.AreEqual(1f, PlayerPrefs.GetFloat("MusicVolume"), "Music volume should be clamped to 1");

        // Открываем меню настроек, чтобы загрузить сохраненные значения
        settingsMenu.OpenSettingsMenu();

        // Ждем 1 кадр, чтобы UI обновился
        yield return null;

        // Проверяем, что значения слайдеров правильные после загрузки из PlayerPrefs
        Assert.AreEqual(0f, soundSlider.value, "Sound volume is not loaded correctly (should be clamped to 0)");
        Assert.AreEqual(1f, musicSlider.value, "Music volume is not loaded correctly (should be clamped to 1)");

        // Проверяем, что переключатели для сложности верны
        Assert.IsTrue(easySwitch.isOn, "Easy mode switch is incorrect (should be on for Easy mode)");
        Assert.IsFalse(hardSwitch.isOn, "Hard mode switch is incorrect (should be off for Easy mode)");

        // Удаляем временные объекты для моков
        UnityEngine.Object.DestroyImmediate(soundManagerMock.gameObject);
        UnityEngine.Object.DestroyImmediate(musicManagerMock.gameObject);
    }

    // Метод для изменения статического свойства Instance синглтона через рефлексию
    private void SetSingletonInstance<T>(string propertyName, T value)
    {
        // Получаем тип синглтона
        Type type = typeof(T);

        // Получаем информацию о свойстве Instance
        PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static);

        // Устанавливаем значение с помощью рефлексии
        propertyInfo.SetValue(null, value);
    }

    // Очистка после каждого теста (Teardown)
    [TearDown]
    public void Teardown()
    {
        // Удаляем объекты, чтобы избежать утечек памяти
        UnityEngine.Object.DestroyImmediate(settingsMenuObject);
        UnityEngine.Object.DestroyImmediate(pauseUI);
        UnityEngine.Object.DestroyImmediate(settingsUI);
        UnityEngine.Object.DestroyImmediate(soundSlider.gameObject);
        UnityEngine.Object.DestroyImmediate(musicSlider.gameObject);
        UnityEngine.Object.DestroyImmediate(easySwitch.gameObject);
        UnityEngine.Object.DestroyImmediate(hardSwitch.gameObject);
    }
}
