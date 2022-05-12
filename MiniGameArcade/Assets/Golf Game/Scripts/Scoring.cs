using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public GameObject StrokesText;
    public GameObject ResultText;

    //private int points = 0;
    //private static bool gameEnded = false;

    //public GameObject gameOverUI;

    public GameObject ScoreNum;


    private int strokes;

    public void addStroke()
    {
        strokes++;
        refreshStrokesText();
    }

    public void completeRound()
    {
        ResultText.SetActive(true);

        //ResultText.GetComponent<TMPro.TextMeshProUGUI>().text = "You finished in " + Score() + " strokes!";
        Score();
        //gameEnded = true;
       // gameOverUI.SetActive(true);
        Debug.Log("Game over");


        ResultText.GetComponent<TMPro.TextMeshProUGUI>().text = "You finished in " + strokes + " strokes!";
        ScoreNum.GetComponent<TMPro.TextMeshProUGUI>().text = strokes.ToString();
        FindObjectOfType<LeaderboardSender>().GetFinalScore();

    }

    private void refreshStrokesText()
    {
        StrokesText.GetComponent<TMPro.TextMeshProUGUI>().text = "Strokes: " + strokes;
    }

    public void Score()
    {
        if (strokes == 1)
        {
            ResultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Hole in One!";
        }

        if (strokes == 2)
        {
            ResultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Bogey!";
        }

        if (strokes == 3)
        {
            ResultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Double Bogey!";
        }

        if (strokes == 4)
        {
            ResultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Triple Bogey!";
        }

        if (strokes == 5)
        {
            ResultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Quadruple Bogey!";
        }

        if (strokes >= 6)
        {
            ResultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Too many strokes :(";
        }

    }
}