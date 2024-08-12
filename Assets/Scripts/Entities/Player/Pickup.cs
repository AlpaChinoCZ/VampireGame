using System;
using Unity.VisualScripting;
using UnityEngine;

namespace VG
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Pickup : MonoBehaviour
    {
        /// <summary>
        /// After a collision with this collider, pick up the item
        /// </summary>
        public SphereCollider TriggerBox { get; private set; }

        protected virtual void Awake()
        {
            TriggerBox = GetComponent<SphereCollider>();
        }
    }
}
