using UnityEngine;

namespace Digging_Game.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        public GameObject player;
        void Update()
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }
}
