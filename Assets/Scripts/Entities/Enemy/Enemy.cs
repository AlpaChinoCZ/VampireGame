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
        [SerializeField] private BasicFire fireComponent;
        [SerializeField] private EnemyController enemyController;

        public EnemyInfo Info => enemyInfo;
        public BasicFire FireComponent => fireComponent;
        public EnemyController EnemyController => enemyController;
        
        private Rigidbody body;
        
        public override void Awake()
        {
            base.Awake();

            body = GetComponent<Rigidbody>();
            
            Assert.IsNotNull(enemyInfo, $"{gameObject} have to have Enemy Info");
            Assert.IsNotNull(fireComponent, $"{gameObject} have to have Fire Component");
        }
    }
}
