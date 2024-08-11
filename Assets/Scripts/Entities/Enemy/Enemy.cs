using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace VG
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : Actor
    {
        [SerializeField] private EnemyInfo enemyInfo;
        [Tooltip("Target towards which the Enemy will move")]
        [SerializeField] private Transform target;
        [SerializeField] private BasicFire fireComponent;

        public Transform Target => target;
        public EnemyInfo Info => enemyInfo;
        public BasicFire FireComponent => fireComponent;
        
        private Rigidbody body;
        private NavMeshAgent navMeshAgent;


        public override void Awake()
        {
            base.Awake();

            body = GetComponent<Rigidbody>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            if (target == null)
            {
                var player = FindObjectOfType(typeof(Player)) as GameObject;
                target = player != null ? player.transform : null;
            }
            
            Assert.IsNotNull(enemyInfo, $"{gameObject} have to have Enemy Info");
            Assert.IsNotNull(target, $"{gameObject} have to have target");
            Assert.IsNotNull(fireComponent, $"{gameObject} have to Fire Component");
        }

        public void Start()
        {
            navMeshAgent.SetDestination(target.transform.position);
        }
    }
}
