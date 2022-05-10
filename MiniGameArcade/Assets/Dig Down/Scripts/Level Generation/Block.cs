using UnityEngine;

/*  Author: Alfredo Hernandez
 *  Description: Block script for blocks
 */

namespace Dig_Down.Scripts.Level_Generation
{
    public class Block : MonoBehaviour
    {
        public float timeValue = 10f;
        public float value;
        
        public string blockName;
        public float health;
        
        public int damageMultiplier = 1;

        private void Start()
        {
            health = 100f;
        }
    }
}
