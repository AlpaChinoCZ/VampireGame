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
    public class Enemy : Actor
    {
        [SerializeField] private EnemyInfo enemyInfo;
        [Tooltip("Target towards which the Enemy will move")]
        [SerializeField] private Transform target;
        [SerializeField] private BasicFire fireComponent;
        [SerializeField] private EnemyMovementController movementComponent;

        public Transform Target => target;
        public EnemyInfo Info => enemyInfo;
        public BasicFire FireComponent => fireComponent;
        public EnemyMovementController EnemyMovementController => movementComponent;
        
        private Rigidbody body;


        public override void Awake()
        {
            base.Awake();

            body = GetComponent<Rigidbody>();
            movementComponent = GetComponent<EnemyMovementController>();

            if (target == null)
            {
                var player = FindObjectOfType(typeof(Player)) as GameObject;
                target = player != null ? player.transform : null;
            }
            
            Assert.IsNotNull(enemyInfo, $"{gameObject} have to have Enemy Info");
            Assert.IsNotNull(target, $"{gameObject} have to have Target");
            Assert.IsNotNull(fireComponent, $"{gameObject} have to have Fire Component");
            Assert.IsNotNull(movementComponent, $"{gameObject} have to have Enemy Movement Component");
        }
    }
}
