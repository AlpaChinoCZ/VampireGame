using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VG
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileInfo projectileInfo;
        private Rigidbody rgBody;

        public void Init(Vector3 direction)
        {
            rgBody.AddForce(projectileInfo.Speed * direction, ForceMode.Impulse);
        }

        private void Awake()
        {
            rgBody = GetComponent<Rigidbody>();
            rgBody.useGravity = false;
        }
    }
}
