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
    [SerializeField] private Button leaderBtn;
    [SerializeField] private MenuNavagation menuNav;
    [SerializeField] private GameObject locked;
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
        PlayMusic();
        playBtn.onClick.RemoveAllListeners();
        leaderBtn.onClick.RemoveAllListeners();
        if (game.ava)
        { 
            locked.SetActive(false);
            playBtn.interactable=true;
            leaderBtn.interactable = true;
            playBtn.onClick.AddListener(() => menuNav.SceneChange(game.gameLevelOneSceneName));
            leaderBtn.onClick.AddListener(() => menuNav.SceneChange(game.leaderboardSceneName));
        }
        else
        {
            locked.SetActive(true);
            playBtn.interactable=false;
            leaderBtn.interactable = false;
        }
    }

    public void PlayMusic()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(backgroundMusic);
    }
}
