using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VG
{
    /// <summary>
    /// Object that lives - has health
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    public abstract class Actor : MonoBehaviour
    {
        private Health health;
        
        public Health Health => health;
        
        protected virtual void Awake()
        {
            health = GetComponent<Health>();
        }
    }
}
