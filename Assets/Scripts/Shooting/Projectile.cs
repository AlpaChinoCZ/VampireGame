using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace VG
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileInfo projectileInfo;
        private LayerMask excludeLayer;
        
        private Rigidbody rgBody;

        /// <summary>
        /// Set correct rotation and force
        /// </summary>
        public virtual void Init(Vector3 direction, LayerMask excludeLayer)
        {
            this.excludeLayer = excludeLayer;
            rgBody.excludeLayers = excludeLayer;
            
            transform.rotation = Quaternion.LookRotation(direction);
            rgBody.AddForce(projectileInfo.Speed * direction.normalized, ForceMode.Impulse);
            Destroy(gameObject, projectileInfo.MaxLifeTime);
        }
        
        protected virtual void OnCollisionEnter(Collision other)
        {
            var damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.ApplyDamage(projectileInfo.Damage);
            }
            
            Destroy(gameObject);
        }
        
        protected virtual void Awake()
        {
            rgBody = GetComponent<Rigidbody>();
            rgBody.useGravity = false;
        }
    }
}
