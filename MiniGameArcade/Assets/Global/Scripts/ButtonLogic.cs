using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLogic : MonoBehaviour
{
    private AudioSource source;
    public AudioClip negAudio;
    public AudioClip posAudio;

   private static ButtonLogic instance;
    // void Awake()
    // {
    //     // Singleton - https://answers.unity.com/questions/1736149/dontdestroyonload-singleton.html && https://forum.unity.com/threads/dontdestroyonload-object-reference-goes-missing-after-scene-change.795126/
    //     if (instance == null) {
    //         instance = this;
    //         DontDestroyOnLoad(gameObject);
    //
    //     } else if (instance != this) {
    //         //Instance is not the same as the one we have, destroy old one, and reset to newest one
    //         Destroy(instance.gameObject);
    //         instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    // }

    void Start()
    {
       
        source =GetComponent<AudioSource>();
        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TextMoveDown(RectTransform rect)
    {
        rect.localPosition += (new Vector3(0,-1,-1));
    }
    public void AudioPlay(AudioClip audioClip)
    {
        source.PlayOneShot(audioClip);

    }

    public void TextMoveUp(RectTransform rect)
    {
        rect.localPosition += (new Vector3(0,1,1)); 
    }

    public void BtnDisable(Button btn)
    {
        btn.interactable = false;
    }
}
