using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VG
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    public abstract class Actor : MonoBehaviour
    {
        private Health health;
        
        public Health Health => health;
        
        public virtual void Awake()
        {
            health = GetComponent<Health>();
        }
    }
}
