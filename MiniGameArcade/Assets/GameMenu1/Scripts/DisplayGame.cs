using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGame : MonoBehaviour
{

    [SerializeField] private TMP_Text gameTitle;
    [SerializeField] private TMP_Text gameInfo;
    [SerializeField] private Image gameImage;
    [SerializeField] private AudioClip backgroundMusic;

    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void  GameSelectionDisplay(GameSelectionScriptable game)
    {
        gameTitle.text = game.gameTitle;
        gameInfo.text = game.gameInfo;
        gameImage.sprite = game.gameImage;
        backgroundMusic = game.backgroundMusic;
        playMusic();
    }

    public void playMusic()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(backgroundMusic);
    }
}