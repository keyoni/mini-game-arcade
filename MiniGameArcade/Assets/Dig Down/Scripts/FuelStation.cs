using UnityEngine;

/*  Author: Alfredo Hernandez
 *  Description: Fuel Station script to handle collision with fuel station
 */

namespace Dig_Down.Scripts
{
    public class FuelStation : MonoBehaviour
    {
        public delegate void Refuel(Vector3 fuelStationPos, Transform fuelTransform, int refuelAmount);
        public event Refuel OnRefuel;

        public bool isFuelExpensive;
        public float fuelCostMultiplier = 1f;
        private void OnTriggerEnter2D(Collider2D col)
        {
            ShipController ship = col.GetComponent<ShipController>();
            if (ship != null)
            {
                var fuelTrans = transform;
                int refuelAmount = 0;

                if (!isFuelExpensive)
                {
                    refuelAmount = (int) ship.maxFuel - (int) ship.fuel;
                }
                else
                {
                    refuelAmount = (int) ship.maxFuel - (int) ship.fuel;
                    float fuelCost = refuelAmount * fuelCostMultiplier;

                    if (fuelCost > ship.score) return;
                    ship.score -= fuelCost;
                }
                OnRefuel?.Invoke(fuelTrans.position, fuelTrans, refuelAmount);
                ship.fuel = ship.maxFuel;
            }
        }
    }
}
