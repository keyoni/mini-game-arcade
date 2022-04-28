using UnityEngine;

// Ref: https://docs.unity3d.com/ScriptReference/Transform.RotateAround.html

namespace Digging_Game.Scripts
{
    public class DayCycle : MonoBehaviour
    {
        void Update()
        {
            transform.RotateAround(transform.position, Vector3.forward, 1 * Time.deltaTime);
        }
    }
}
