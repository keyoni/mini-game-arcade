using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StartMenu.Scripts
{
    public class MenuAudio : MonoBehaviour
    {
        private AudioSource _audioSource;
        private GameObject[] other;
        private bool NotFirst = false;
        private void Awake()
        {
            other = GameObject.FindGameObjectsWithTag("Music");
 
            foreach (GameObject oneOther in other)
            {
                if (oneOther.scene.buildIndex == -1)
                {
                    NotFirst = true;
                }
            }
 
            if (NotFirst == true)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(transform.gameObject);
            _audioSource = GetComponent<AudioSource>();
        }
    
        public void PlayMusic()
        {
            if (_audioSource.isPlaying) return;
            _audioSource.Play();
        }
 
        public void StopMusic()
        {
            _audioSource.Stop();
        }

        private void Update()
        {
            
            Scene scene = SceneManager.GetActiveScene();
            Debug.Log("this is: + " + scene.name);
            if (scene.name == "GameSelection")
            {
                StopMusic();
            }
            else
            {
                PlayMusic();
            }
        }
    }
}