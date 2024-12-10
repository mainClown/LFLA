using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using System;
using System.Collections;

[TestFixture]
public class SettingsMenuTests
{
    // ���� ��� ��������
    private GameObject settingsMenuObject;
    private SettingsMenu settingsMenu;
    private GameObject pauseUI;
    private GameObject settingsUI;
    private Slider soundSlider;
    private Slider musicSlider;
    private Toggle easySwitch;
    private Toggle hardSwitch;

    // ����� ��� ���������� ����� (Setup)
    [SetUp]
    public void Setup()
    {
        // ������� ������ SettingsMenu
        settingsMenuObject = new GameObject("SettingsMenu");
        settingsMenu = settingsMenuObject.AddComponent<SettingsMenu>();

        // ������� ������� UI
        pauseUI = new GameObject("PauseUI");
        settingsUI = new GameObject("SettingsUI");
        soundSlider = new GameObject("SoundSlider").AddComponent<Slider>();
        musicSlider = new GameObject("MusicSlider").AddComponent<Slider>();
        easySwitch = new GameObject("EasySwitch").AddComponent<Toggle>();
        hardSwitch = new GameObject("HardSwitch").AddComponent<Toggle>();

        // ����������� ������� UI � ����������� SettingsMenu
        settingsMenu.PauseUI = pauseUI;
        settingsMenu.SettingsUI = settingsUI;
        settingsMenu.SoundSlider = soundSlider;
        settingsMenu.MusicSlider = musicSlider;
        settingsMenu.EasySwitch = easySwitch;
        settingsMenu.HardSwitch = hardSwitch;

        // �������� ��������� �������� � PlayerPrefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("SoundVolume", 40f / 100f);
        PlayerPrefs.SetFloat("MusicVolume", 60f / 100f);
        PlayerPrefs.SetInt("EasyMode", 0);
        PlayerPrefs.Save();
    }

    [UnityTest]
    public IEnumerator B5_OpenSettingsMenu_ShowsCorrectValues()
    {
        // ��������� ���� ��������
        settingsMenu.OpenSettingsMenu();

        // ���� ���� ����, ����� ��������� �������� � ����
        yield return null;

        // ���������, ��� SettingsUI ����� �������� (���� �������)
        Assert.IsTrue(settingsUI.activeSelf, "Settings UI is not visible");

        // ���������, ��� �������� ����� ���������� ��������
        Assert.AreEqual(0.4f, soundSlider.value, "Sound volume is incorrect");
        Assert.AreEqual(0.6f, musicSlider.value, "Music volume is incorrect");

        // ���������, ��� ������������� ��������� ���������
        Assert.IsFalse(easySwitch.isOn, "Easy mode switch is incorrect (should be off for Hard mode)");
        Assert.IsTrue(hardSwitch.isOn, "Hard mode switch is incorrect (should be on for Hard mode)");
    }

    [Test]
    public void B5a_OpenSettingsMenu_InvalidPrefabName()
    {
        // ������� ��������� �������������� ������
        GameObject settingsMenuObject = Resources.Load<GameObject>("SettingsMenu_Missing");

        // ���������, ��� ������ � ����� ������ �� ������
        Assert.IsNull(settingsMenuObject, "Prefab 'SettingsMenu_Missing' should not exist in Resources.");

        try
        {
            // �������� ��������� �������� � �������������� ��������
            SettingsMenu settingsMenuScript = settingsMenuObject.GetComponent<SettingsMenu>();

            // �������� �����, ������� ������ ��������� ����������
            settingsMenuScript.OpenSettingsMenu();

            // ���� ���������� �� �����������, ���� ��������
            Assert.Fail("Expected NullReferenceException was not thrown.");
        }
        catch (NullReferenceException)
        {
            // ���� ����������� NullReferenceException, �� ���� ��������
            Assert.Pass("NullReferenceException thrown as expected.");
        }
        catch (Exception ex)
        {
            // ���� ������������� ������ ����������, ���� ��������
            Assert.Fail($"Unexpected exception thrown: {ex.Message}");
        }
    }

    // ����� ��� ������� ����� ����� (Teardown)
    [TearDown]
    public void Teardown()
    {
        // ������� ��� ��������� ������� ����� �����
        UnityEngine.Object.DestroyImmediate(settingsMenuObject); // ����������: ���������� UnityEngine.Object
    }
}
