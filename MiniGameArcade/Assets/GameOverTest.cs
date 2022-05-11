using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverTest : MonoBehaviour
{

    // Time management
    private float _timeAccumulated;
    private float _countDownAccumulated;
    public float timeLeft = 20f;
    public TMP_Text timer;
    public TMP_Text counter;
    public GameObject leaderboardSend;
    public bool gameover = false;
    //public static event Action GameEnds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            UpdateTimer();
        }
        
    }

    public void  GameOverHappens()
    {
        //GameEnds?.Invoke();
        gameover = true;
        counter.gameObject.SetActive(false);
        leaderboardSend.GetComponent<LeaderboardSender>().GetFinalScore();
        //ZFindObjectOfType<LeaderboardSender>().GetFinalScore();
    }
    
    private void UpdateTimer()
    {
        _countDownAccumulated += Time.deltaTime;
        
        if (_countDownAccumulated > 0.01f)
        {
            timeLeft -= 0.01f;
            timer.text = timeLeft.ToString("00");
            _countDownAccumulated = 0f;
        }
        if(timeLeft <= 0f)
        {
            timer.text = "GAME OVER";
            GameOverHappens();
        }
    }
}
