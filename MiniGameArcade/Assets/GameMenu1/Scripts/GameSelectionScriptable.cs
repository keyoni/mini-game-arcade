using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu (fileName = "New GameSelect", menuName= "ScriptableObject/GameSelect" )]
public class GameSelectionScriptable : ScriptableObject
{
     public int gameIndex;
     public String gameTitle;
     public String gameInfo;
     public Sprite gameImage;
     public String leaderboardSceneName;
     public String gameLevelOneSceneName;
     public AudioClip backgroundMusic;
     public bool ava;
}
