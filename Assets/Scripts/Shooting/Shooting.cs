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
        [SerializeField] private LayerMask hitLayer;
        
        /// <summary>
        /// Launch projectile from start to end direction
        /// </summary>
        public virtual void Launch(Vector3 startPosition, Vector3 targetPosition)
        {
            var startDir = new Vector3(startPosition.x, 0, startPosition.z);
            var endDir = new Vector3(targetPosition.x, 0, targetPosition.z);
            var projectilePrefab = Instantiate(projectile, startPosition, Quaternion.identity);
            var direction = endDir - startDir;
            projectilePrefab.Init(direction, hitLayer);
        }
    }
}
