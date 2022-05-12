using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class testScore : MonoBehaviour
{
    public TMP_Text scoreText;

    private int score = 0;
    //public static event Action<int> ScoreSend;
    // Start is called before the first frame update
    void Start()
    {
       //GameOverTest.GameEnds += Reset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OneUp()
    {
        score++;
        scoreText.text = score.ToString();
    }
    
    public int GetScore()
    {
        return score;
    }
    public void Reset()
    {
        score = 0;
        
    }
}
