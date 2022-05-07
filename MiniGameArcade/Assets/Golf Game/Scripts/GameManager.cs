using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private static bool gameEnded = false;

    public GameObject gameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        Start();
        
    }
    public void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
        Debug.Log("Game over");

    }
}