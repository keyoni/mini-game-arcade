using System;
using TMPro;
using UnityEngine;

namespace Digging_Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public float gameTick = 2f;
        public TextMeshProUGUI currentFuel;
        public TextMeshProUGUI currentScore;
        public TextMeshProUGUI currentDepth;
        public ShipController ship;
        
        // Time management
        private float _timeAccumulated;

        private void Start()
        {
            ship = FindObjectOfType<ShipController>();
        }

        private void Update()
        {
            UpdateFuel();
            // TODO: Make fuel text flash on low fuel
            currentFuel.color = ship.fuel <= 25 ? Color.red : Color.yellow;
            currentFuel.text = $"Fuel: {ship.fuel} L";
            currentScore.text = $"Score: {ship.score}";
            currentDepth.text = ship.depth > 0 ? $"Depth: -{ship.depth} ft." : $"Depth: {ship.depth} ft.";
        }

        private void UpdateFuel()
        {
            _timeAccumulated += Time.deltaTime;

            if (_timeAccumulated > gameTick)
            {
                if (ship.fuel > 0)
                {
                    ship.fuel -= ship.fuelLostPerTick;
                    _timeAccumulated = 0f;
                }
                else
                {
                    ship.fuel = 0;
                }
                
            }
        }
    }
}
