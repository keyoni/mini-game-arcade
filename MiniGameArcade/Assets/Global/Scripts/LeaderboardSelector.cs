using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardSelector : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] leaderboardToSelect;
    [SerializeField] private LeaderboardDisplay displayLeaderboard;
    private int _index;


    private void Awake()
    {
        _index = 0;
        changeSelectedLeaderboard(0);
    }

    public void changeSelectedLeaderboard(int change)
    {
        _index += change;

        //If user goes pass the first leaderboard, loop back to the last one.
        if (_index < 0) 
        {
            _index = leaderboardToSelect.Length - 1;
            //If user goes pass the last leaderboard, loop back to the first one.
        } else if (_index > leaderboardToSelect.Length - 1 )
        {
            _index = 0;
        }

        //Display leaderboard at current index
        if (displayLeaderboard != null)
        {
            displayLeaderboard.DisplayLeaderboard((LeaderboardScriptable) leaderboardToSelect[_index]);
        }
    }


}

