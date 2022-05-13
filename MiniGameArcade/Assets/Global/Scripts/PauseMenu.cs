using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = true;
    // Start is called before the first frame update
    void Start()
    {
       // DontDestroyOnLoad(gameObject);
        Close();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused == false)
        {
            if (Input.GetKey(KeyCode.Escape))
            {

                Open();

            }
        }
        // } else if (!isPaused)
        // {
        //     if (Input.GetKey(KeyCode.Escape))
        //     {
        //    
        //         Open();
        //         
        //     }
        // }
    }

    public void Close()
    {
       this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-10000, -100000,-100000);
       
      
       Time.timeScale = 1;
       //this.gameObject.SetActive(false);
       isPaused = false;
       //StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.05f);
        isPaused = !isPaused;
    }

    private void Open()
    {
        //this.gameObject.SetActive(true);
        //isPaused = !isPaused;
        this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0,0);
        Time.timeScale = 0;
        isPaused = true;
    }
    
    
}
