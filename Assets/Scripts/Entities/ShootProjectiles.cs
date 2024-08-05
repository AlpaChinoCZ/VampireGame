using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VG
{
    public class ShootProjectiles : MonoBehaviour
    {
        [SerializeField] private Projectile projectile;
        
        public void Launch(Vector3 targetPosition)
        {
            var projectilePrefab = Instantiate(projectile, transform.position, Quaternion.identity);
            var direction = transform.position - targetPosition;
            projectilePrefab.Init(direction);
        }
    }
}
