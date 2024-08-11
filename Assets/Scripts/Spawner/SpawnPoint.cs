using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace VG
{
    [DisallowMultipleComponent]
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Enemy[] enemies;
        [SerializeField] private float spawnRadius = 1f;

        public Enemy[] Enemies => enemies;
        public float SpawnRadius => spawnRadius;

        private void Awake()
        {
            Assert.IsTrue(enemies.Length > 0, $"{gameObject} have no enemies to spawn");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, spawnRadius);
        }
    }
}
