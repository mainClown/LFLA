using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    public static float MusicVolume { get; private set; }
    private Dictionary<MusicClip, AudioClip> audioClips;
    private AudioSource audioSource;
    public enum MusicClip
    {
        //Названия музыки в папке Resources/Music
    }

    #region Singleton паттерн
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            LoadAudioClips();
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            DontDestroyOnLoad(target: this);
        }
    }
    #endregion
    public void UpdateVolume()
    {
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    // Метод для загрузки звуков в словарь
    void LoadAudioClips()
    {
        audioClips = new Dictionary<MusicClip, AudioClip>();

        // Загружаем все аудиоклипы в папке Resources/Music
        foreach (MusicClip clip in System.Enum.GetValues(typeof(MusicClip)))
        {
            AudioClip audioClip = Resources.Load<AudioClip>("Music/" + clip);
            if (audioClip != null)
            {
                audioClips[clip] = audioClip;
            }
            else
            {
                Debug.LogWarning("AudioClip not found for " + clip);
            }
        }
    }

    // Метод поиска аудиоклипа по перечислению
    public AudioClip GetAudioClip(MusicClip soundClip)
    {
        if (audioClips.TryGetValue(soundClip, out AudioClip clip))
        {
            return clip; // Возвращаем клип если найден
        }
        else
        {
            Debug.LogWarning("AudioClip for " + soundClip + " not found.");
            return null; // Если не найдено, возвращаем null
        }
    }

    // Метод воспроизведения звука
    public void PlayMusic(MusicClip soundClip)
    {
        AudioClip clip = GetAudioClip(soundClip);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, MusicVolume);
        }
    }
}

