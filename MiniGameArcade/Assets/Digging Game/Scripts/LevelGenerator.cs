using UnityEngine;
using Random = UnityEngine.Random;

// Generates a level using perlin noise.
// Helpful resource: https://www.youtube.com/watch?v=bG0uEXV6aHQ

namespace Digging_Game.Scripts
{
    public class LevelGenerator : MonoBehaviour
    {
        public GameObject dirtPrefab;
        public GameObject stonePrefab;
        public GameObject orePrefab;
        
        public Transform levelRoot;

        public int levelHeight = 10;
        public int levelWidth = 20;

        public float scale = 25f;
        void Start()
        {
            GenerateLevel();
        }

        void GenerateLevel()
        {
            // TODO: Add different ores.
            // TODO: Make frequency of ores higher the the more ft you dig.
            // TODO: Spawn certain ores past certain depth.
            for (int row = 0; row < levelHeight; row++)
            {
                for (int column = 0; column < levelWidth; column++)
                {
                    if (row == levelHeight - 1)
                    {
                        var block = Instantiate(dirtPrefab, levelRoot);
                        block.transform.position = new Vector3(column + 0.5f, row + 0.5f, 0f);
                    }
                    else
                    {
                        CreateBlock(row, column);
                    }
                }
            }
        }
        
        void CreateBlock(int row, int column)
        {
            // Different scale every time game loads
            float randomScale = Random.Range(scale + 10f, scale + 9000f);
            
            float r = (float) row / levelHeight * randomScale;
            float h = (float) column / levelWidth * randomScale;
            
            float perlinOutput = Mathf.PerlinNoise(r, h);
            Vector3 blockOffsetPos = new Vector3(column + 0.5f, row + 0.5f, 0f);
            
            if (perlinOutput < 0.20f)
            {
                var block = Instantiate(orePrefab, levelRoot);
                block.transform.position = blockOffsetPos;
            } 
            else if (perlinOutput < 0.35f)
            {
                var block = Instantiate(stonePrefab, levelRoot);
                block.transform.position = blockOffsetPos;
            }
            else
            {
                var block = Instantiate(dirtPrefab, levelRoot);
                block.transform.position = blockOffsetPos;
            }
        }
    }
}
