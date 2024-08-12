using System;
using Unity.VisualScripting;
using UnityEngine;

namespace VG
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Pickup : MonoBehaviour
    {
        public SphereCollider TriggerBox { get; private set; }

        protected virtual void Awake()
        {
            TriggerBox = GetComponent<SphereCollider>();
        }
    }
}
