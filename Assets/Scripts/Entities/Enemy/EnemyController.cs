using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace VG
{
    /// <summary>
    /// Takes care of all enemy actions such as shooting, movement, etc.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        [Tooltip("How often the enemy will be looking for the target's position - is seconds")]
        [SerializeField] private float targetCheckInterval = 0.1f;
        [Tooltip("Target towards which the Enemy will move \nIf the target is empty, the player class is used instead")]
        [SerializeField] private Transform target;
        [SerializeField] private float rotationSpeed = 0.15f;

        public NavMeshAgent Agent => navMeshAgent;
            
        private Enemy enemy;
        private NavMeshAgent navMeshAgent;
        private WaitForSeconds rapidFireWait;
        private WaitForSeconds waitForCheck;
        
        private bool isFiring = false;

        public void SetNewTarget(Transform newTarget)
        {
            target = newTarget;
        }
        
        protected virtual void Awake()
        {
            enemy = GetComponent<Enemy>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            if (target == null)
            {
                var player = GameManager.Instance.Player;
                target = player != null ? player.transform : null;
            }
            
            Assert.IsNotNull(target, $"{gameObject} have to have Target");
            Assert.IsNotNull(enemy, $"{gameObject} have to have Enemy component");
            Assert.IsTrue(targetCheckInterval > 0, $"{gameObject} Target Check Interval must be bigger than 0s");

            navMeshAgent.speed = enemy.Info.Speed;
            waitForCheck = new WaitForSeconds(targetCheckInterval);
            rapidFireWait = new WaitForSeconds(1f / enemy.Info.FireRate);
        }
        
        protected virtual void Update()
        {
            transform.SmoothLookAt(target.position, rotationSpeed);
            if (IsInFireDistance() && !isFiring)
            {
                StartCoroutine(FiringCoroutine());
            }
        }
        
        protected virtual void OnEnable()
        {
            navMeshAgent.SetDestination(target.transform.position);
            StartCoroutine(CheckTargetPositionCoroutine());
        }

        protected virtual void OnDisable()
        {
            StopAllCoroutines();
        }
        protected virtual IEnumerator FiringCoroutine()
        {
            isFiring = true;

            while (IsInFireDistance())
            {
                enemy.FireComponent.Launch(target.position);
                yield return rapidFireWait;
            }

            isFiring = false;
        }
        
        protected virtual IEnumerator CheckTargetPositionCoroutine()
        {
            while (true)
            {
                navMeshAgent.SetDestination(target.transform.position);
                yield return waitForCheck;
            }
        }

        private bool IsInFireDistance()
        {
            return Vector3.Distance(transform.position, target.position) <= enemy.Info.StartFireDistance;
        }
    }
}
