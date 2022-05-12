using UnityEngine;
using Random = UnityEngine.Random;

/*
 *  Author: Alfredo Hernandez
 *  Description: Copy of level generation script. Modified by Philip
 *  For use in level 3 scene. Includes different ore types
 */

namespace Dig_Down.Scripts.Level_Generation
{
    public class Level3 : MonoBehaviour
    {
        public GameObject airPrefab;
        public GameObject dirtPrefab;
        public GameObject stonePrefab;
        public GameObject mythrilPrefab;
        public GameObject ironPrefab;
        public GameObject rubyPrefab;
        public GameObject emeraldPrefab;
        public GameObject obsidianPrefab;
        public GameObject sapphirePrefab;
        public Transform levelRoot;

        public int levelHeight = 10;
        public int levelWidth = 20;

        public float baseScale = 25f;
        void Start()
        {
            GenerateLevel();
        }

        void GenerateLevel()
        {
            for (int row = 0; row < levelHeight; row++)
            {
                for (int column = 0; column < levelWidth; column++)
                {
                    CreateBlock(row, column);
                }
            }
        }
        
        void CreateBlock(int row, int column)
        {
            // Different scale every time game loads
            float randomScale = Random.Range(baseScale + 10f, baseScale + 9000f);
            
            float r = (float) row / levelHeight * randomScale;
            float h = (float) column / levelWidth * randomScale;
            
            float perlinOutput = Mathf.PerlinNoise(r, h);
            Vector3 blockOffsetPos = new Vector3(column + 0.5f, row + 0.5f, 0f);

            GameObject blockToCreate;
            
            if (column == 0 || column == levelWidth - 1)
            {
                blockToCreate = obsidianPrefab;
            }
            else
            {
                if (row == levelHeight - 1)
                {
                    blockToCreate = dirtPrefab;
                }
                else if (row > 0)
                {
                    switch (perlinOutput)
                    {
                        case < 0.07f when row < levelHeight / 3:
                            blockToCreate = emeraldPrefab;
                            break;
                        case < 0.10f when row < levelHeight / 2:
                            blockToCreate = rubyPrefab;
                            break;
                        case < 0.12f when row < levelHeight / 2:
                            blockToCreate = sapphirePrefab;
                            break;
                        case < 0.15f when row < levelHeight - 10:
                            blockToCreate = ironPrefab;
                            break;
                        case < 0.20f:
                            blockToCreate = mythrilPrefab;
                            break;
                        case < 0.25f when row < levelHeight - 2:
                            blockToCreate = airPrefab;
                            break;
                        case < 0.35f:
                            blockToCreate = stonePrefab;
                            break;
                        default:
                            blockToCreate = dirtPrefab;
                            break;
                    }
                }
                else
                {
                    // Spawn obsidian at very bottom of level
                    blockToCreate = obsidianPrefab;
                }
            }
            Instantiate(blockToCreate, blockOffsetPos, Quaternion.identity, levelRoot);
        }
    }
}
