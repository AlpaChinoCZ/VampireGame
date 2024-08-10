using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace VG
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : Actor
    {
        [SerializeField] private PlayerFire fireComponent;
        [SerializeField] private MovementController movementController;
        
        public PlayerFire FireComponent=> fireComponent;
        public MovementController MovementController => movementController;

        private Rigidbody body;

        public override void Awake()
        {
            base.Awake();

            body = GetComponent<Rigidbody>();
            movementController = GetComponent<MovementController>();
            
            Assert.IsNotNull(MovementController, $"{gameObject} movement component is null");
            Assert.IsNotNull(fireComponent, $"{gameObject} launch projectile is null");
        }
    }
}
