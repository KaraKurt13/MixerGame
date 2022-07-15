using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private Sprite turnedOffMusicImage;
    [SerializeField] private Sprite turnedOnMusicImage;
    [SerializeField] private Button musicControlButton;
    [SerializeField] private Image musicStatusImage;
    [SerializeField] private AudioClip backgroundMusic;
    private AudioSource musicSource;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        PlayBackgroundMusic();
        musicControlButton.onClick.AddListener(delegate { TurnOffMusic(); });
    }

    public void TurnOffMusic()
    {
        musicSource.Stop();
        musicControlButton.onClick.AddListener(delegate { TurnOnMusic(); });
        musicStatusImage.sprite = turnedOffMusicImage;
    }

    public void TurnOnMusic()
    {
        musicSource.Play();
        musicControlButton.onClick.AddListener(delegate { TurnOffMusic(); });
        musicStatusImage.sprite = turnedOnMusicImage;
    }

    private void PlayBackgroundMusic()
    {
        musicSource.loop = true;
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }
}
