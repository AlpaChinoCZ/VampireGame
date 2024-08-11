using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace VG
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        
        [Tooltip("How often the enemy will be looking for the target's position - is seconds")]
        [SerializeField] private float targetCheckInterval = 0.1f;
        
        private Enemy enemy;
        private NavMeshAgent navMeshAgent;
        private WaitForSeconds rapidFireWait;
        private WaitForSeconds waitForCheck;
        
        private bool isFiring = false;

        protected virtual void Awake()
        {
            enemy = GetComponent<Enemy>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            
            Assert.IsNotNull(enemy, $"{gameObject} have to have Enemy component");
            Assert.IsTrue(targetCheckInterval > 0, $"{gameObject} Target Check Interval must be bigger than 0s");

            navMeshAgent.speed = enemy.Info.Speed;
            waitForCheck = new WaitForSeconds(targetCheckInterval);
            rapidFireWait = new WaitForSeconds(1f / enemy.Info.FireRate);
        }

        protected virtual void Update()
        {
            if (IsInFireDistance() && !isFiring)
            {
                StartCoroutine(FiringCoroutine());
            }
        }
        
        protected void OnEnable()
        {
            navMeshAgent.SetDestination(enemy.Target.transform.position);
            StartCoroutine(CheckTargetPositionCoroutine());
        }

        protected virtual void OnDisable()
        {
            StopAllCoroutines();
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
        
        protected virtual IEnumerator CheckTargetPositionCoroutine()
        {
            while (true)
            {
                navMeshAgent.SetDestination(enemy.Target.transform.position);
                yield return waitForCheck;
            }
        }

        private bool IsInFireDistance()
        {
            return Vector3.Distance(transform.position, enemy.Target.position) <= enemy.Info.StartFireDistance;
        }
    }
}
