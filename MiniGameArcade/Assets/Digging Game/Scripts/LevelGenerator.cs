using UnityEngine;
using Random = UnityEngine.Random;

// Generates a level using perlin noise.
// Helpful resource: https://www.youtube.com/watch?v=bG0uEXV6aHQ

namespace Digging_Game.Scripts
{
    public class LevelGenerator : MonoBehaviour
    {
        public GameObject airPrefab;
        public GameObject dirtPrefab;
        public GameObject stonePrefab;
        public GameObject copperPrefab;
        public GameObject ironPrefab;
        public GameObject goldPrefab;
        public GameObject diamondPrefab;
        public GameObject obsidianPrefab;
        
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

            if (row == levelHeight - 1)
            {
                blockToCreate = dirtPrefab;
            }
            else if (row > 0)
            {
                if (perlinOutput < 0.07f && row < levelHeight / 3)
                {
                    blockToCreate = diamondPrefab;
                }
                else if (perlinOutput < 0.10f && row < levelHeight / 2)
                {
                    blockToCreate = goldPrefab;
                }
                else if (perlinOutput < 0.15f && row < levelHeight - 10)
                {
                    blockToCreate = ironPrefab;
                }
                else if (perlinOutput < 0.20f)
                {
                    blockToCreate = copperPrefab;
                }
                else if (perlinOutput < 0.25f && row < levelHeight - 2)
                {
                    blockToCreate = airPrefab;
                }
                else if (perlinOutput < 0.35f)
                {
                    blockToCreate = stonePrefab;
                }
                else
                {
                    blockToCreate = dirtPrefab;
                }
            }
            else
            {
                // Spawn obsidian at very bottom of level
                blockToCreate = obsidianPrefab;
            }
            
            Instantiate(blockToCreate, blockOffsetPos, Quaternion.identity, levelRoot);
        }
    }
}