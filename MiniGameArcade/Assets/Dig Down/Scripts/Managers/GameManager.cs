using Dig_Down.Scripts.Level_Generation;
using UnityEngine;

/*  Author: Alfredo Hernandez
 *  Game manager to manage changes in game state.
 */

namespace Dig_Down.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public float gameTick = 2f;
        public ShipController ship;
        public ShipMining shipMining;
        private Vector3 _surfacePos;
        
        // Time management
        private float _timeAccumulated;
        private float _countDownAccumulated;
        public float timeLeft = 200f;
        
        public delegate void UpdateUI(ShipController ship, float timeLeft);
        public event UpdateUI OnUIUpdate;

        public delegate void GameOver(Vector3 shipPos);
        public event GameOver OnGameOver;

        public int oresToMine = 5;
        public string oreName = "Iron";

        private void Start()
        {
            ship = FindObjectOfType<ShipController>();
            shipMining = ship.GetComponent<ShipMining>();
            _surfacePos = FindObjectOfType<FuelStation>().transform.position;
            shipMining.OnBlockMined += UpdateOresLeft;
            //shipMining.OnBlockMined += UpdateGameTimer;
        }

        private void Update()
        {
            if (ship.health <= 0f || timeLeft <= 0f || ship.fuel <= 0f || oresToMine <= 0)
            {
                shipMining.OnBlockMined -= UpdateOresLeft;
                OnGameOver?.Invoke(ship.transform.position);
            }
            else
            {
                OnUIUpdate?.Invoke(ship, timeLeft);
                UpdateShipDepth();
                UpdateFuel();
                UpdateTimer();
            }
        }

        // Update time remaining until game is over using block mined value.
        /*private void UpdateGameTimer(Block block, Vector3 shipPos)
        {
            timeLeft += block.timeValue * Time.deltaTime;
        }*/

        private void UpdateOresLeft(Block block, Vector3 shipPos)
        {
            if (block.blockName == oreName)
            {
                oresToMine--;
            }
        }

        // Update distance of ship from the surface
        private void UpdateShipDepth()
        {
            var shipPos = ship.transform.position;
            if (shipPos.y < _surfacePos.y)
            {
                var difference = _surfacePos.y - shipPos.y;
                ship.depth = (int) (difference * 12f);
            } else if (shipPos.y >= _surfacePos.y)
            {
                ship.depth = 0;
            }
        }

        // Update level countdown
        private void UpdateTimer()
        {
            _countDownAccumulated += Time.deltaTime;

            if (_countDownAccumulated > 0.01f)
            {
                timeLeft -= 0.01f;
                _countDownAccumulated = 0f;
            }
        }

        // Update fuel counter
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
