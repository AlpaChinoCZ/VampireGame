using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VG
{
    [CreateAssetMenu(fileName = "ProjectileInfo", menuName = "VG/Info/ProjectileInfo")]
    public class ProjectileInfo : ScriptableObject
    {
        [Tooltip("Damage the enemy deals to the player")]
        [SerializeField] private float damage = 5f;
        [Tooltip("Speed of the projectile")]
        [SerializeField] private float speed = 50f;
        [Tooltip("Color of the projectile")]
        [SerializeField] private Color color = Color.blue;
        
        public float Damage => damage;
        public float Speed => speed;
        public Color Color => color;
    }
}
