using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace VG
{
    [DisallowMultipleComponent]
    public class EnemyController : MonoBehaviour
    {
        private Enemy enemy;
        private WaitForSeconds rapidFireWait;
        
        private bool isFiring = false;
        private Coroutine fireCoroutine;

        protected virtual void Awake()
        {
            enemy = GetComponent<Enemy>();
            
            Assert.IsNotNull(enemy, $"{gameObject} has no Enemy component");
            
            rapidFireWait = new WaitForSeconds( 1f / enemy.Info.FireRate);
        }

        protected virtual void Update()
        {
            if (IsInFireDistance() && !isFiring)
            {
                fireCoroutine = StartCoroutine(FiringCoroutine());
            }
        }

        private IEnumerator FiringCoroutine()
        {
            isFiring = true;

            while (IsInFireDistance())
            {
                enemy.FireComponent.Launch(enemy.Target.position);
                yield return rapidFireWait;
            }

            isFiring = false;
        }

        private bool IsInFireDistance()
        {
            return Vector3.Distance(transform.position, enemy.Target.position) <= enemy.Info.StartFireDistance;
        }
    }
}
