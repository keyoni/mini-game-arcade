using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu (fileName = "New Leaderboard", menuName= "ScriptableObject/LeaderboardScriptable" )]
public class LeaderboardScriptable : ScriptableObject
{
    public int lbIndex;
    public String levelTitle;
    public String playerPrefsBase;
}
