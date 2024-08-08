using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VG
{
    public class LaunchProjectile : MonoBehaviour, IProjectile
    {
        [SerializeField] private Projectile projectile;
        
        public void Launch(Vector3 startPosition, Vector3 targetPosition)
        {
            var startDir = new Vector3(startPosition.x, 0, startPosition.z);
            var endDir = new Vector3(targetPosition.x, 0, targetPosition.z);
            var projectilePrefab = Instantiate(projectile, startPosition, Quaternion.identity);
            var direction = endDir - startDir;
            projectilePrefab.Init(direction);
        }
    }
}
