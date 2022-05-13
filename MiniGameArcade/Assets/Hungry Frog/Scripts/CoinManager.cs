using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CoinManager : MonoBehaviour
{
    public int coins = 1;
    public GameObject coinsText;
    public GameObject timerText;
    public GameObject winnerMessage;
    private bool timerIsActive = true;
    private float currentTime = 0;

    void Update() {
        if (timerIsActive) {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timerText.GetComponent<TMPro.TextMeshProUGUI>().text = time.ToString(@"mm\:ss\:ff");
    }

    public void coinGet() {
        coins--;
        coinsText.GetComponent<TMPro.TextMeshProUGUI>().text = ""+coins;
        FindObjectOfType<FrogAudio>().playSound("coin");
        if (coins<=0) {
            allCoinsGot();
        }
    }

    private void allCoinsGot() {
        timerIsActive = false;
        winnerMessage.SetActive(true);
        FindObjectOfType<FrogAudio>().playSound("win");
        FindObjectOfType<LeaderboardSender>().GetFinalScore();

    }
}
