using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using VG;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 randomSpawnInterval = new Vector2(2f, 5f);
    
    private EnemySpawnPoint[] enemySpawnPoints;
    
    private void Start()
    {
        // using FindObjectsOfType is okay here. Its called just once at start
        enemySpawnPoints = FindObjectsOfType<EnemySpawnPoint>();
        
        Assert.IsFalse(enemySpawnPoints.Length > 0, "There must be at least one spawn point");
        
        if (enemySpawnPoints.Length > 0)
        {
            StartCoroutine(SpawnCoroutine());

            List<Enemy> enemies = new();
            foreach (var spawnObj in enemySpawnPoints)
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
            var spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
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
