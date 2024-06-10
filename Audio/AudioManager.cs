using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip menuMusic;
    public AudioClip selectUI;
    public AudioClip healing;
    public AudioClip praying;
    public AudioClip castSpell;
    public AudioClip death;
    public AudioClip enemyAttack;
    public AudioClip pickup;

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more than one AudioManager. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        PlayMusic(menuMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
        Debug.Log("Playing sound " + clip.ToString());
    }
}
