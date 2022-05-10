using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LeaderboardSender: MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private TMP_InputField nameInput;

    public TMP_Text currentScore;

    private int score;

    [SerializeField] private GameObject yourScore;

    [SerializeField] private GameObject NameScoreSubmit;
    // Start is called before the first frame update
    private int highScoreRank = -1;
    void Start()
    {
        this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0,-100000);
        GameOverTest.GameEnds += Active;
        GameOverTest.GameEnds += GetFinalScore;
        
        CreatingLeaderboard();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Active()
    {
        this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0,0);
        //gameObject.SetActive(true);
        //GetFinalScore();
        
    }
    public void GetFinalScore()
    {
        scoreText.text = currentScore.text;
        score = Int32.Parse(currentScore.text);
        if (CheckIfHighScore())
        {
            print(highScoreRank);
         
        };
        Active();
    }

     bool CheckIfHighScore()
    {
      //ToDo: If High score, do high score animation
        string playerPrefsBase = this.gameObject.scene.name;
        if (score > PlayerPrefs.GetInt(playerPrefsBase + "[1].score"))
        {
            highScoreRank = 1;
            return true;
        }
        else if (score > PlayerPrefs.GetInt(playerPrefsBase + "[2].score"))
        {
            highScoreRank = 2;
            return true;
      
        } else if (score > PlayerPrefs.GetInt(playerPrefsBase + "[3].score"))
        {
            highScoreRank = 3;
            return true;
        }

        return false;
    }

     public void SetHighScore()
    {
        string playerPrefsBase = this.gameObject.scene.name;
        switch (highScoreRank)
        {
          case 1:
              //shift scores 2->3
              PlayerPrefs.SetString(playerPrefsBase + "[3].name", PlayerPrefs.GetString(playerPrefsBase + "[2].name"));
              PlayerPrefs.SetInt(playerPrefsBase + "[3].score", PlayerPrefs.GetInt(playerPrefsBase + "[2].score"));
              //shift scores 1->2
              PlayerPrefs.SetString(playerPrefsBase + "[2].name", PlayerPrefs.GetString(playerPrefsBase + "[1].name"));
              PlayerPrefs.SetInt(playerPrefsBase + "[2].score", PlayerPrefs.GetInt(playerPrefsBase + "[1].score"));
              
              PlayerPrefs.SetString(playerPrefsBase + "[1].name", nameInput.text);
              PlayerPrefs.SetInt(playerPrefsBase + "[1].score", score);
              break;
          case 2:
              //shift scores 2->3
              PlayerPrefs.SetString(playerPrefsBase + "[3].name", PlayerPrefs.GetString(playerPrefsBase + "[2].name"));
              PlayerPrefs.SetInt(playerPrefsBase + "[3].score", PlayerPrefs.GetInt(playerPrefsBase + "[2].score"));
              
              PlayerPrefs.SetString(playerPrefsBase + "[2].name", nameInput.text);
              PlayerPrefs.SetInt(playerPrefsBase + "[2].score", score);
              break;
          case 3:
              PlayerPrefs.SetString(playerPrefsBase + "[3].name", nameInput.text);
              PlayerPrefs.SetInt(playerPrefsBase + "[3].score", score);
              break;
        }
    }
    

//TODO move to Leaderboard pages 
    //https://forum.unity.com/threads/leaderboard-script-using-playerprefs.257900/
    private void CreatingLeaderboard()
    {
        string playerPrefsBase = this.gameObject.scene.name;
       
        if (!PlayerPrefs.HasKey(playerPrefsBase))
        {
            PlayerPrefs.SetString(playerPrefsBase,"true");
            for (int i = 1; i <= 3; i++)
            {
                //PlayerPrefs.SetString(PlayerPrefsBaseKey + "[" + i + "].name", entry.name);
                PlayerPrefs.SetString(playerPrefsBase + "[" + i + "].name","void");
                PlayerPrefs.SetInt(playerPrefsBase + "[" + i + "].score",0);
            }
        }

    }
}
