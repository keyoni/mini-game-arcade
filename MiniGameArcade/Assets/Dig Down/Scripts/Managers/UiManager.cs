using Dig_Down.Scripts.Level_Generation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*  Author: Alfredo Hernandez
 *  Description: UI manager to handle updates to the UI
 */

namespace Dig_Down.Scripts.Managers
{
    public class UiManager : MonoBehaviour
    {
        private ShipController _ship;
        private ShipMining _shipMining;
        private GameManager _gm;
        private FuelStation _fuelStation;
        
        // Spawn text when mining block showing block name/value
        public GameObject textObj;
        private TextMeshProUGUI _block;

        public GameObject gameOverObj;
        private TextMeshProUGUI _gameOver;

        // TextMeshProUGUI references
        public TextMeshProUGUI levelCountdown;
        public TextMeshProUGUI depth;
        public TextMeshProUGUI score;
        public TextMeshProUGUI fuel;
        public TextMeshProUGUI health;

        public TextMeshProUGUI hiddenScore;

        public TextMeshProUGUI oresLeft;

        
        // Bars
        public Image fuelBarFill;
        public Image healthBarFill;

        private void Start()
        {
            _ship = FindObjectOfType<ShipController>();
            _shipMining = _ship.GetComponent<ShipMining>();
            _shipMining.OnBlockMined += OnBlockMined;

            _gm = FindObjectOfType<GameManager>();
            _gm.OnUIUpdate += OnUIUpdate;
            _gm.OnGameOver += GameOver;

            _fuelStation = FindObjectOfType<FuelStation>();
            _fuelStation.OnRefuel += OnRefuel;
            
            oresLeft.text = $"{_gm.oresToMine} {_gm.oreName} left!";
        }

        private void GameOver(Vector3 shipPos)
        {
            health.text = $"Health: 0.00";
            var pos = new Vector3(shipPos.x, shipPos.y + 1.5f, shipPos.z);
            var text = Instantiate(gameOverObj, pos, Quaternion.identity, _ship.transform);

            //hiddenScore.text = score.text;
            FindObjectOfType<LeaderboardSender>().GetFinalScore();

            var textMesh = text.GetComponentInChildren<TextMeshProUGUI>();
            if (_gm.oresToMine <= 0)
            {
                textMesh.text = "You Win!";
                textMesh.color = Color.green;
                oresLeft.text = "";
            }
            else
            {
                textMesh.color = Color.red;
            }


            _gm.OnGameOver -= GameOver;
        }
        
        private void OnUIUpdate(ShipController ship, float timeLeft)
        {
            fuel.color = ship.fuel <= 25 ? Color.red : Color.yellow;
            fuel.text = $"Fuel: {ship.fuel} L";
            fuelBarFill.fillAmount = Mathf.Clamp(ship.fuel / ship.maxFuel, 0f, 1f);
            fuelBarFill.color = ship.fuel <= 25 ? Color.red : Color.green;
            
            score.text = $"Score: {ship.score}";
            hiddenScore.text = ship.score.ToString();
            depth.text = ship.depth > 0 ? $"Depth: -{ship.depth} ft." : $"Depth: {ship.depth} ft.";
            
            levelCountdown.text = $"Time Left: {timeLeft:0.00}";
            levelCountdown.color = timeLeft <= 25 ? Color.red : Color.yellow;
            
            health.color = ship.health <= 35 ? Color.red : Color.green;
            health.text = $"Health: {ship.health:0.00}";
            healthBarFill.fillAmount = Mathf.Clamp(ship.health / ship.maxHealth, 0f, 1f);

            oresLeft.text = $"{_gm.oresToMine} {_gm.oreName} left!";
        }
        
        private void OnBlockMined(Block block, Vector3 shipPos)
        {
            // ======================= Spawn text object showing mined blocks info =====================================
            var blockValue = Instantiate(textObj, shipPos, Quaternion.identity, _ship.transform);
            _block = blockValue.GetComponentInChildren<TextMeshProUGUI>();
            _block.text = $"{block.value}";
            _block.color = Color.green;
            Destroy(blockValue, 0.5f);

            if (block.value > 25)
            {
                var namePos = new Vector3(shipPos.x, shipPos.y - 0.5f, shipPos.z);
                var blockName = Instantiate(textObj, namePos, Quaternion.identity, _ship.transform);
                _block = blockName.GetComponentInChildren<TextMeshProUGUI>();
                _block.text = $"+1 {block.blockName}";
                _block.color = Color.yellow;
                Destroy(blockName, 0.5f);
            }
        }

        private void OnRefuel(Vector3 fuelStationPos, Transform fuelTransform, int refuelAmount)
        {
            var fuelTextObj = Instantiate(textObj, fuelStationPos, Quaternion.identity, fuelTransform.GetChild(0).transform);
            var fuelText = fuelTextObj.GetComponentInChildren<TextMeshProUGUI>();
            fuelTextObj.transform.position = new Vector2(fuelStationPos.x + 3.5f, fuelStationPos.y + 1.5f);
            fuelText.text = $"+{refuelAmount}L";
            fuelText.color = Color.red;
            Destroy(fuelTextObj, 0.5f);
        }
    }
}