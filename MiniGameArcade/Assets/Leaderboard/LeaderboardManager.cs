using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CreatingLeaderboard(String playerPrefsBase )
    {
        //string playerPrefsBase = this.gameObject.scene.name;
       
        if (!PlayerPrefs.HasKey(playerPrefsBase))
        {
            
            PlayerPrefs.SetString(playerPrefsBase,"true");
            if (playerPrefsBase.Contains("Alex") || playerPrefsBase.Contains("Kevin"))
            {
                for (int i = 1; i <= 3; i++)
                {
                    //PlayerPrefs.SetString(PlayerPrefsBaseKey + "[" + i + "].name", entry.name);
                    PlayerPrefs.SetString(playerPrefsBase + "[" + i + "].name","void");
                    PlayerPrefs.SetInt(playerPrefsBase + "[" + i + "].score",999);
                }
            }
            else
            {
                for (int i = 1; i <= 3; i++)
                {
                    //PlayerPrefs.SetString(PlayerPrefsBaseKey + "[" + i + "].name", entry.name);
                    PlayerPrefs.SetString(playerPrefsBase + "[" + i + "].name", "void");
                    PlayerPrefs.SetInt(playerPrefsBase + "[" + i + "].score", 0);
                }
            }
        }

    }

    public void DeleteLeaderboardData()
    {
        
        PlayerPrefs.DeleteAll();
    }
}

