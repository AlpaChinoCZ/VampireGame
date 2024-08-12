using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace VG
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : Actor
    {
        [Tooltip("Fire rate which is applied if an enemy is nearby")]
        [SerializeField] private float fireRate = 2f;
        [SerializeField] private BasicFire fireComponent;
        [SerializeField] private MovementController movementController;
        [SerializeField] private SphereCollider sphereTrigger;
        [SerializeField] private LayerMask nearestObjectLayer;
        
        public BasicFire FireComponent=> fireComponent;
        public MovementController MovementController => movementController;
        public HashSet<Transform> NearestObjects => nearestObjects;
        public float FireRate => fireRate;
        
        private Rigidbody body;
        private HashSet<Transform> nearestObjects;

        public override void Awake()
        {
            base.Awake();

            body = GetComponent<Rigidbody>();
            movementController = GetComponent<MovementController>();
            nearestObjects = new HashSet<Transform>();
             
            Assert.IsNotNull(sphereTrigger, $"{gameObject} sphere trigger is null");
            Assert.IsNotNull(MovementController, $"{gameObject} movement component is null");
            Assert.IsNotNull(fireComponent, $"{gameObject} launch projectile is null");
            Assert.IsTrue(fireRate > 0, $"{gameObject} Fire Rate must be bigger than 0s");
            
            sphereTrigger.isTrigger = true;
        }
        
        public virtual void OnTriggerEnter(Collider other)
        {
            if (nearestObjectLayer.Contains(other.gameObject.layer))
            {
                nearestObjects.Add(other.gameObject.transform);
            }
        }
        
        public virtual void OnTriggerExit(Collider other)
        {
            nearestObjects.Remove(other.gameObject.transform);
        }

        public Transform GetNearestObject()
        {
            var currentPosition = transform.position;
            Transform closesTransform = null;
            var oldDistance = Mathf.Infinity;
            
            foreach (var tr in nearestObjects)
            {
                var dist = Vector3.Distance(currentPosition, tr.position);
                if (dist < oldDistance)
                {
                    closesTransform = tr;
                    oldDistance = dist;
                }
            }

            return closesTransform;
        }

        public void RemoveNearestObject(Transform transform)
        {
            nearestObjects.Remove(transform);
        }
        
        private void OnEnable()
        {
            Health.onDead.AddListener(VgGameManager.Instance.OnPlayerDied);
        }
        
        private void OnDisable()
        {
            Health.onDead.RemoveListener(VgGameManager.Instance.OnPlayerDied);
        }
    }
}