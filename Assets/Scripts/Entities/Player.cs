using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace VG
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Player : Actor
    {
        [SerializeField] private BasicFire fireComponent;
        [SerializeField] private MovementController movementController;
        [SerializeField] private LayerMask nearestObjectLayer;
        
        public BasicFire FireComponent=> fireComponent;
        public MovementController MovementController => movementController;
        public HashSet<GameObject> NearestObjects => nearestObjects;
        
        

        private Rigidbody body;
        private SphereCollider sphereTrigger;
        private HashSet<GameObject> nearestObjects;

        public override void Awake()
        {
            base.Awake();

            body = GetComponent<Rigidbody>();
            movementController = GetComponent<MovementController>();
            sphereTrigger = GetComponent<SphereCollider>();
            sphereTrigger.isTrigger = true;
            nearestObjects = new HashSet<GameObject>();
             
            Assert.IsNotNull(MovementController, $"{gameObject} movement component is null");
            Assert.IsNotNull(fireComponent, $"{gameObject} launch projectile is null");
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            if (nearestObjectLayer.Contains(other.gameObject.layer))
            {
                nearestObjects.Add(other.gameObject);
            }
        }
        public virtual void OnTriggerExit(Collider other)
        {
            nearestObjects.Remove(other.gameObject);
        }
    }
}
