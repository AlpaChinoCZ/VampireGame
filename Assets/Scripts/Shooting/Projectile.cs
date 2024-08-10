using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace VG
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileInfo projectileInfo;
        private LayerMask hitLayer;
        
        private Rigidbody rgBody;

        /// <summary>
        /// Set correct rotation and force
        /// </summary>
        public virtual void Init(Vector3 direction, LayerMask hitLayer)
        {
            this.hitLayer = hitLayer;
            transform.rotation = Quaternion.LookRotation(direction);
            rgBody.AddForce(projectileInfo.Speed * direction, ForceMode.Impulse);
        }
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.ApplyDamage(projectileInfo.Damage);
                Destroy(this);
                return;
            }

            if (other.gameObject.layer == hitLayer)
            {
                Destroy(this);
            }
        }
        
        protected virtual void Awake()
        {
            rgBody = GetComponent<Rigidbody>();
            rgBody.useGravity = false;
        }
    }
}
