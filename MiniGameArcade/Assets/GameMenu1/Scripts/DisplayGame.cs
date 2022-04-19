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
    [SerializeField] private Button playBtn;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private MenuNavagation menuNav;
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
        playBtn.onClick.RemoveAllListeners();
        playBtn.onClick.AddListener(() =>menuNav.SceneChange(game.gameLevelOneSceneName));
    }

    public void playMusic()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(backgroundMusic);
    }
}
