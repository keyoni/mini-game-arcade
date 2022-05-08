using UnityEngine;

/*  Author: Alfredo Hernandez
 *  Fuel Station script to handle collision with fuel station
 */

namespace Dig_Down.Scripts
{
    public class FuelStation : MonoBehaviour
    {
        public delegate void Refuel(Vector3 fuelStationPos, Transform fuelTransform, int refuelAmount);
        public event Refuel OnRefuel;
        // TODO: Add options to buy units of fuel or fill entire tank.
        private void OnTriggerEnter2D(Collider2D col)
        {
            ShipController ship = col.GetComponent<ShipController>();
            if (ship != null)
            {
                var fuelTrans = transform;
                int refuelAmount = (int) ship.maxFuel - (int) ship.fuel;
                OnRefuel?.Invoke(fuelTrans.position, fuelTrans, refuelAmount);
                ship.fuel = ship.maxFuel;
            }
        }
    }
}
