using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
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
        }
    }

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

    private Enemy GetRandomEnemy(EnemySpawnPoint spawnPoint)
    {
        return spawnPoint.Enemies[Random.Range(0, spawnPoint.Enemies.Length)];
    }
}
