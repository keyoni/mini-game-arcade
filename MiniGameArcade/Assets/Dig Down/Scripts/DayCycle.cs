using UnityEngine;

/*  Author: Alfredo Hernandez
 *  Day cycle script
 */

// Ref: https://docs.unity3d.com/ScriptReference/Transform.RotateAround.html

namespace Dig_Down.Scripts
{
    public class DayCycle : MonoBehaviour
    {
        void Update()
        {
            transform.RotateAround(transform.position, Vector3.forward, 1 * Time.deltaTime);
        }
    }
}
