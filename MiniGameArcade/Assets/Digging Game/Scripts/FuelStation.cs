using System;
using UnityEngine;

namespace Digging_Game.Scripts
{
    public class FuelStation : MonoBehaviour
    {
        // TODO: Add options to buy units of fuel or fill entire tank.
        private void OnTriggerEnter2D(Collider2D col)
        {
            ShipController ship = col.GetComponent<ShipController>();
            if (ship != null)
            {
                ship.fuel = ship.maxFuel;
            }
        }
    }
}
