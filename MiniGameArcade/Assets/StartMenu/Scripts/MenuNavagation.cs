using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavagation : MonoBehaviour
{
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (!this.gameObject.scene.name.Equals("StartMenu"))
        {
            if (!FindObjectOfType<PauseMenu>())
            { 
                Instantiate(pauseMenu, canvas.gameObject.transform); 
                print("Scene:" + this.gameObject.scene.name);

            }
        }
    }

    // Update is called once per frame
         void Update()
         {

         }

         public void SceneChange(String sceneName)
         {
             //StartCoroutine(Wait(sceneName));
             SceneManager.LoadScene(sceneName);
         }

         IEnumerator Wait(String sceneName)
         {
        
             yield return new WaitForSeconds(0.5f);
        
             SceneManager.LoadScene(sceneName);
         }

         public void RestartScene()
         {
             SceneManager.LoadScene(SceneManager.GetActiveScene().name);
         }

         public void ExitGame()
         {
             print("Exit Game");
             Application.Quit();
         }
}

