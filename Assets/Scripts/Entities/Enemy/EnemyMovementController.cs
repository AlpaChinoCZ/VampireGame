using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace VG
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovementController : MonoBehaviour
    {
        [Tooltip("How often the enemy will be looking for the target's position - is seconds")]
        [SerializeField] private float targetCheckInterval = 0.1f;
        
        private NavMeshAgent navMeshAgent;
        private Enemy enemy;
        private WaitForSeconds waitForCheck;
        private Coroutine checkTargetCoroutine;
        
        protected virtual void Awake()
        {
            enemy = GetComponent<Enemy>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            
            Assert.IsNotNull(enemy, $"{gameObject} have to have ");
            Assert.IsTrue(targetCheckInterval > 0, $"{gameObject} Target Check Interval must be bigger than 0s");

            navMeshAgent.speed = enemy.Info.Speed;
            waitForCheck = new WaitForSeconds(targetCheckInterval);
        }
        
        
        protected virtual void OnEnable()
        {
            navMeshAgent.SetDestination(enemy.Target.transform.position);
            checkTargetCoroutine = StartCoroutine(CheckTargetPositionCoroutine());
        }

        protected virtual void OnDisable()
        {
            StopCoroutine(checkTargetCoroutine);
        }

        protected virtual IEnumerator CheckTargetPositionCoroutine()
        {
            while (true)
            {
                navMeshAgent.SetDestination(enemy.Target.transform.position);
                yield return waitForCheck;
            }
        }
    }
}
