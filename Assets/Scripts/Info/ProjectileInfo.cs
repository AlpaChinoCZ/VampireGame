using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VG
{
    [CreateAssetMenu(fileName = "ProjectileInfo", menuName = "VG/Info/ProjectileInfo")]
    public class ProjectileInfo : ScriptableObject
    {
        [Tooltip("Damage the projectile deals to the player")]
        [SerializeField] private float damage = 5f;
        [Tooltip("Speed of the projectile")]
        [SerializeField] private float speed = 50f;
        
        public float Damage => damage;
        public float Speed => speed;
    }
}
