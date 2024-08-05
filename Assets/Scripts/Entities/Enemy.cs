using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VG
{
    public class Enemy : Actor
    {
        [SerializeField] private EnemyInfo enemyInfo;

        public EnemyInfo Info => enemyInfo;

        public UnityEvent onShoot;

        public override void Awake()
        {
            base.Awake();
    
        }
    }
}
