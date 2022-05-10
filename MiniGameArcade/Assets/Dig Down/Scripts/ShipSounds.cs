using UnityEngine;

namespace Dig_Down.Scripts
{
    public class ShipSounds : MonoBehaviour
    {
        private void Update()
        {
            Dig_Down.Scripts.Managers.AudioManager.PlaySound("shipIdle");
        }
    }
}
