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
        //�������� ������ � ����� Resources/Music
    }

    #region Singleton �������
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

    // ����� ��� �������� ������ � �������
    void LoadAudioClips()
    {
        audioClips = new Dictionary<MusicClip, AudioClip>();

        // ��������� ��� ���������� � ����� Resources/Music
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

    // ����� ������ ���������� �� ������������
    public AudioClip GetAudioClip(MusicClip soundClip)
    {
        if (audioClips.TryGetValue(soundClip, out AudioClip clip))
        {
            return clip; // ���������� ���� ���� ������
        }
        else
        {
            Debug.LogWarning("AudioClip for " + soundClip + " not found.");
            return null; // ���� �� �������, ���������� null
        }
    }

    // ����� ��������������� �����
    public void PlayMusic(MusicClip soundClip)
    {
        AudioClip clip = GetAudioClip(soundClip);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, MusicVolume);
        }
    }
}

