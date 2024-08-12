using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VG
{
    public class BasicFire : MonoBehaviour, ILaunchable
    {
        [SerializeField] private Projectile projectile;
        [SerializeField] private Transform startPosition;
        [Tooltip("Layer which will projectile ignore")]
        [SerializeField] private LayerMask excludeLayer;
        
        public Transform StartPosition => startPosition;
        
        /// <summary>
        /// Launch projectile from start to end (direction)
        /// </summary>
        public GameObject Launch(Vector3 targetPosition)
        {
            var position = startPosition.position;
            var startDir = new Vector3(position.x, 0, position.z);
            var endDir = new Vector3(targetPosition.x, 0, targetPosition.z);
            var projectilePrefab = Instantiate(projectile, position, Quaternion.identity);
            var direction = endDir - startDir;
            
            projectilePrefab.Init(direction, excludeLayer);
            return projectilePrefab.gameObject;
        }
    }
}
