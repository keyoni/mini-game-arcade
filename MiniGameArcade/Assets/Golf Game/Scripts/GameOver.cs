using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private static bool gameEnded = false;
    public GameObject gameOverUI;

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("The button was pressed!");

    }
    public static void NextLevel()
    {
        {
            SceneManager.LoadScene("AlexLevel1");
            Debug.Log("This is the new level");
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject collisionGameObject = col.gameObject;

        if (collisionGameObject.name == "Golf Ball")
        {
            //LoadScene();
        }
    }


}