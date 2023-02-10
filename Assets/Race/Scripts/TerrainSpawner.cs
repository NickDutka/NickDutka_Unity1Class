using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    public GameObject[] terrainTiles;
    public float spawnInterval = 10f;
    public int numberOfPrefabsToSpawn = 10;
    public float spawnPositionZ = 0f;

    private void Start()
    {
        for (int i = 0; i < numberOfPrefabsToSpawn; i++)
        {
            int randomIndex = Random.Range(0, terrainTiles.Length);
            Vector3 spawnPosition = new Vector3(0, 0, spawnPositionZ);
            Instantiate(terrainTiles[randomIndex], spawnPosition, Quaternion.identity);
            spawnPositionZ += spawnInterval;
        }
    }
}
