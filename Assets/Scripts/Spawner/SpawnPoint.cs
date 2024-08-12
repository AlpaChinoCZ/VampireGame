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
        [SerializeField] private float spawnRadius = 1f;
        [SerializeField] private Color color = Color.green;

        public float SpawnRadius => spawnRadius;

        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, spawnRadius);
        }
    }
}
