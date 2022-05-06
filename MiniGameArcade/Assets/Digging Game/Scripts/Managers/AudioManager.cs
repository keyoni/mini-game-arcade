using System;
using UnityEngine;

/*  Author: Alfredo Hernandez
 *  Audio manager to manage sounds in game
 */

namespace Digging_Game.Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioClip _shipIdle, _fuelLow, _shipDrill;
        private static AudioSource _audioSource1, _audioSource2;
        private ShipController _ship;
        private Rigidbody2D _shipRb;

        public float minEnginePitch = 1f;
        public float maxEnginePitch = 3.5f;

        void Start()
        {
            AudioSource[] audioSources = GetComponents<AudioSource>();
            _audioSource1 = audioSources[0];
            _audioSource2 = audioSources[1];
            
            _fuelLow = Resources.Load<AudioClip>("Sounds/DigDown/fuel_low");
            _shipDrill = Resources.Load<AudioClip>("Sounds/DigDown/ship_drill1");
            _shipIdle = Resources.Load<AudioClip>("Sounds/DigDown/ship_engine");
 
            _ship = FindObjectOfType<ShipController>();
            _shipRb = _ship.GetComponent<Rigidbody2D>();
            _audioSource1.pitch = minEnginePitch;
        }

        private void Update()
        {
            UpdateEngineSound();
        }
        public static void PlaySound(string clipName)
        {
            switch (clipName)
            {
                case "fuelLow":
                    //_audioSource1.PlayOneShot(_fuelLow);
                    break;
                case "shipDrill":
                    _audioSource2.loop = true;
                    _audioSource2.clip = _shipDrill;
                    _audioSource2.Play();
                    break;
                case "shipIdle":
                    _audioSource1.loop = true;
                    _audioSource1.clip = _shipIdle;
                    _audioSource1.PlayScheduled(AudioSettings.dspTime + _shipIdle.length);
                    break;
            }
        }

        public static void StopSound(string clipName)
        {
            switch (clipName)
            {
                case "fuelLow":
                    _audioSource1.Stop();
                    break;
                case "shipDrill":
                    _audioSource2.Stop();
                    break;
                case "shipIdle":
                    _audioSource1.Stop();
                    break;
            }
        }
        
        private void UpdateEngineSound()
        {
            /*if (_ship.mineDown)
            {
                return;
            }*/
            
            // TODO: Fix this and find better sounds.
            var shipVerticalSpeed = (_shipRb.velocity.magnitude * 3f) / _ship.speed;
            var shipSpeed = (Math.Abs(_ship.movement) * _ship.speed * Time.deltaTime) * 5f;

            /*if (!_ship.shipGrounded || _ship.movement == 0f)
            {
                shipSpeed = shipVerticalSpeed;
            }*/
            
            if (shipSpeed < minEnginePitch)
            {
                _audioSource1.pitch = minEnginePitch;
            }
            else
            {
                _audioSource1.pitch = shipSpeed;
            }
            /*if (shipSpeed > maxEnginePitch)
            {
                _audioSource1.pitch = maxEnginePitch;
            }*/
        }
    }
}
