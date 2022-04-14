using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public GameObject StrokesText;
    public GameObject ResultText;

    private int strokes;

    public void addStroke() {
        strokes++;
        refreshStrokesText();
    }

    public void completeRound() {
        ResultText.SetActive(true);
        ResultText.GetComponent<TMPro.TextMeshProUGUI>().text = "You finished in " + strokes + " strokes!";
    }

    private void refreshStrokesText() {
        StrokesText.GetComponent<TMPro.TextMeshProUGUI>().text = "Strokes: " + strokes;
    }
}
