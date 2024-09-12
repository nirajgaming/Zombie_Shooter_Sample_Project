using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public Transform[] SpawnPoints;
    public GameObject normalZombiePrefab;
    public GameObject bossZombiePrefab;
    public int bossZombieInterval = 5;
    private int normalZombieCount = 0;

    private void OnEnable()
    {
        EventManager.GameStarted += OnGameStarted;
        EventManager.EnemyKilled += SpawnZombie;
    }

    private void OnDisable()
    {
        EventManager.GameStarted -= OnGameStarted;
        EventManager.EnemyKilled -= SpawnZombie;
    }

    void OnGameStarted()
    {
        SpawnZombie();
    }

    public void SpawnZombie()
    {
        if (normalZombieCount < bossZombieInterval)
        {
            // Get a normal zombie from PoolManager
            GameObject normalZombie = SpawnManager.Instance.GetNormalZombie();
            normalZombie.transform.position = GetRandomSpawnPoint();
            normalZombie.SetActive(true);
            normalZombieCount++;
        }
        else
        {
            // Get a boss zombie from PoolManager
            GameObject bossZombie = SpawnManager.Instance.GetBossZombie();
            bossZombie.transform.position = GetRandomSpawnPoint();
            bossZombie.SetActive(true);
            normalZombieCount = 0;
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 newSpwanPoint = Vector3.zero;
        // Your spawn point logic
        if (SpawnPoints.Length > 0)
        {
            newSpwanPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)].position;
        }

        return newSpwanPoint;
    }
}
