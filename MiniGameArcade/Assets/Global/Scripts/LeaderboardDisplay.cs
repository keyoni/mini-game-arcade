using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text gameTitle;
    [SerializeField] private TMP_Text firstName;
    [SerializeField] private TMP_Text firstScore;
    [SerializeField] private TMP_Text secondName;
    [SerializeField] private TMP_Text secondScore;
    [SerializeField] private TMP_Text thirdName;
    [SerializeField] private TMP_Text thirdScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  public  void DisplayLeaderboard(LeaderboardScriptable lb)
    {
        gameTitle.text = lb.levelTitle;
        
        firstName.text =  PlayerPrefs.GetString(lb.playerPrefsBase + "[1].name"); 
        firstScore.text = PlayerPrefs.GetInt(lb.playerPrefsBase + "[1].score").ToString();
        secondName.text = PlayerPrefs.GetString(lb.playerPrefsBase + "[2].name"); 
        secondScore.text = PlayerPrefs.GetInt(lb.playerPrefsBase + "[2].score").ToString();
        thirdName.text = PlayerPrefs.GetString(lb.playerPrefsBase + "[3].name");
        thirdScore.text = PlayerPrefs.GetInt(lb.playerPrefsBase + "[3].score").ToString();
        
    }
}
