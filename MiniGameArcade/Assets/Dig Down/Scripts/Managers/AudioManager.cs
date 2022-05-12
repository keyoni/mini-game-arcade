using System;
using UnityEngine;

/*  Author: Alfredo Hernandez
 *  Description: Audio manager to manage sounds in game
 */

namespace Dig_Down.Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioClip _shipIdle, _shipDrill;
        private static AudioSource _src1, _src2, _src3;
        private ShipController _ship;
        private Rigidbody2D _shipRb;
        
        private static AudioClip _musicClip1;

        public float minEnginePitch = 1f;
        public float maxEnginePitch = 3.5f;

        void Start()
        {
            AudioSource[] audioSources = GetComponents<AudioSource>();
            _src1 = audioSources[0];
            _src2 = audioSources[1];
            _src3 = audioSources[2];
            
            _shipIdle = Resources.Load<AudioClip>("DigDown/Sounds/ship_engine");
 
            _ship = FindObjectOfType<ShipController>();
            _shipRb = _ship.GetComponent<Rigidbody2D>();
            _src1.pitch = minEnginePitch;
            
            _shipDrill = Resources.Load<AudioClip>("DigDown/Sounds/Drill/drill6");
            
            // Ambience
            _musicClip1 = Resources.Load<AudioClip>("DigDown/Sounds/Ambience/space_ambience");
        }

        private void Update()
        {
            //UpdateEngineSound();
        }
        public static void PlaySound(string clipName)
        {
            switch (clipName)
            {
                case "shipDrill":
                    _src2.clip = _shipDrill;
                    _src2.loop = true;
                    _src2.Play();
                    break;
                case "shipIdle":
                    _src1.loop = true;
                    _src1.clip = _shipIdle;
                    _src1.PlayScheduled(AudioSettings.dspTime + _shipIdle.length);
                    break;
                case "ambience":
                    _src3.loop = true;
                    _src3.clip = _musicClip1;
                    _src3.Play();
                    break;
            }
        }

        public static void StopSound(string clipName)
        {
            switch (clipName)
            {
                case "shipDrill":
                    _src2.Stop();
                    break;
                case "shipIdle":
                    _src1.Stop();
                    break;
            }
        }
        
        /*private void UpdateEngineSound()
        {
            // TODO: Fix this and find better sounds.
            var shipVerticalSpeed = (_shipRb.velocity.magnitude * 3f) / _ship.speed;
            var shipSpeed = (Math.Abs(_ship.movement) * _ship.speed * Time.deltaTime) * 5f;

            /*if (!_ship.shipGrounded || _ship.movement == 0f)
            {
                shipSpeed = shipVerticalSpeed;
            }#1#
            
            if (shipSpeed < minEnginePitch)
            {
                _src1.pitch = minEnginePitch;
            }
            else
            {
                _src1.pitch = shipSpeed;
            }
            /*if (shipSpeed > maxEnginePitch)
            {
                _audioSource1.pitch = maxEnginePitch;
            }#1#
        }*/
    }
}
