using UnityEngine;
using System.Collections.Generic;

public class TickSpawner : MonoBehaviour
{
    [SerializeField] private TickManager tickManager;
    [SerializeField] private Transform spawnPoint; 

    [System.Serializable]
    public class TickPrefabPair
    {
        public int tickNumber; 
        public GameObject[] prefabs; 
    }

    [SerializeField] private TickPrefabPair[] tickSpawnData; 

    private Dictionary<int, List<GameObject>> tickSpawnRules = new Dictionary<int, List<GameObject>>();
    private int lastTick = 0;

    void Start()
    {
        foreach (var pair in tickSpawnData)
        {
            if (!tickSpawnRules.ContainsKey(pair.tickNumber))
            {
                tickSpawnRules[pair.tickNumber] = new List<GameObject>();
            }

            tickSpawnRules[pair.tickNumber].AddRange(pair.prefabs);
        }
    }

    void Update()
    {
        if (tickManager.tickCount > lastTick)
        {
            lastTick = tickManager.tickCount;
            CheckForSpawn(lastTick);
        }
    }

    void CheckForSpawn(int currentTick)
    {
        if (tickSpawnRules.ContainsKey(currentTick))
        {
            List<GameObject> possibleSpawns = tickSpawnRules[currentTick];

            if (possibleSpawns.Count > 0)
            {
                int randomIndex = Random.Range(0, possibleSpawns.Count);
                SpawnObject(possibleSpawns[randomIndex]);
            }
        }
    }

    void SpawnObject(GameObject prefab)
    {
        Vector3 spawnPosition = spawnPoint ? spawnPoint.position : transform.position;
        Instantiate(prefab, spawnPosition, Quaternion.identity);
        Debug.Log($"Spawned {prefab.name} en tick {lastTick}");
    }
}
