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
        [SerializeField] private LayerMask hitLayer;
        
        public Transform StartPosition => startPosition;
        
        /// <summary>
        /// Launch projectile from start to end direction
        /// </summary>
        public void Launch(Vector3 targetPosition)
        {
            var position = startPosition.position;
            var startDir = new Vector3(position.x, 0, position.z);
            var endDir = new Vector3(targetPosition.x, 0, targetPosition.z);
            var projectilePrefab = Instantiate(projectile, position, Quaternion.identity);
            var direction = endDir - startDir;
            
            projectilePrefab.Init(direction, hitLayer);
        }
    }
}
