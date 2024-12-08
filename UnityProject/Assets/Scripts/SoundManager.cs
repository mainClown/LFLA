using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public static float SoundVolume { get; private set; }
    private Dictionary<SoundClip, AudioClip> audioClips;
    private AudioSource audioSource;
    public enum SoundClip
    {
        ButtonClick,
        ItemPick,
        DoorCreak,
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
            SoundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
            DontDestroyOnLoad(target: this);
        }
    }
    #endregion
    public void UpdateVolume() 
    {
        SoundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
    }

    // Метод для загрузки звуков в словарь
    void LoadAudioClips()
    {
        audioClips = new Dictionary<SoundClip, AudioClip>();

        // Загружаем все аудиоклипы в папке Resources/Sounds
        foreach (SoundClip clip in System.Enum.GetValues(typeof(SoundClip)))
        {
            AudioClip audioClip = Resources.Load<AudioClip>("SFX/" + clip);
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
    public AudioClip GetAudioClip(SoundClip soundClip)
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
    public void PlaySound(SoundClip soundClip)
    {
        AudioClip clip = GetAudioClip(soundClip);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, SoundVolume);
        }
    }
}
