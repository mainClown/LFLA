using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
public class SettingsMenu : MonoBehaviour
{
    //ссылки на объекты префаба
    public GameObject PauseUI;
    public GameObject SettingsUI;
    public Slider SoundSlider;
    public Slider MusicSlider;
    public Toggle EasySwitch;
    public Toggle HardSwitch;
    //переменные
    private float SoundVolume = 1f;
    private float MusicVolume = 1f;
    private bool EasyMode = true;

    public void SaveSettings()
    {
        SoundVolume = SoundSlider.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
        PlayerPrefs.Save();
        MusicVolume = MusicSlider.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.Save();
        EasyMode = EasySwitch.isOn;
        PlayerPrefs.SetInt("EasyMode", Convert.ToInt32(EasyMode));
        PlayerPrefs.Save();
        SoundManager.Instance.UpdateVolume();
        MusicManager.Instance.UpdateVolume();
    }
    public void OpenSettingsMenu() 
    {
        SoundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        EasyMode = Convert.ToBoolean(PlayerPrefs.GetInt("EasyMode", 1));
        PauseUI.SetActive(false);
        SettingsUI.SetActive(true);
        SoundSlider.GetComponent<Slider>().value = SoundVolume;
        MusicSlider.GetComponent<Slider>().value = MusicVolume;
        EasySwitch.isOn = EasyMode;
        HardSwitch.isOn = !EasyMode;
    }
    public void CloseSettingsMenu() 
    {
        PauseUI.SetActive(true);
        SettingsUI.SetActive(false);
    }
}
