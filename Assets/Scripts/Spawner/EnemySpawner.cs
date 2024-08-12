using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using VG;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 randomSpawnInterval = new Vector2(2f, 5f);
    
    private EnemySpawnPoint[] spawnObjects;
    
    private void Start()
    {
        spawnObjects = FindObjectsOfType<EnemySpawnPoint>();
        if (spawnObjects.Length > 0)
        {
            StartCoroutine(SpawnCoroutine());

            List<Enemy> enemies = new();
            foreach (var spawnObj in spawnObjects)
            {
                enemies.AddRange(spawnObj.Enemies);
            }
            
            VgGameManager.Instance.InitKillCounter(enemies.ToArray());
        }
    }

    /// <summary>
    /// Spawning of enemies in given interval
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(randomSpawnInterval.x, randomSpawnInterval.y));
            var spawnPoint = spawnObjects[Random.Range(0, spawnObjects.Length)];
            if (spawnPoint != null)
            {
                SpawnEnemy(spawnPoint);
            }
        }
    }

    /// <summary>
    /// Spawn new enemy at Spawn Point
    /// </summary>
    private Enemy SpawnEnemy(EnemySpawnPoint spawnPoint)
    {
        NavMeshHit hitResult;
        if (NavMesh.SamplePosition(spawnPoint.transform.position, out hitResult, 1.0f, NavMesh.AllAreas))
        {
            var newEnemy = Instantiate(GetRandomEnemy(spawnPoint), hitResult.position, Quaternion.identity);
            newEnemy.EnemyController.Agent.Warp(hitResult.position);
            return newEnemy;
        }

        return null;
    }
    
    /// <summary>
    /// Get random enemy from Spawn Point
    /// </summary>
    private Enemy GetRandomEnemy(EnemySpawnPoint spawnPoint)
    {
        return spawnPoint.Enemies[Random.Range(0, spawnPoint.Enemies.Length)];
    }
}
