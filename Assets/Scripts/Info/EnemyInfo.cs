using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VG
{
    [CreateAssetMenu(fileName = "ActorInfo", menuName = "VG/Info/ActorInfo")]
    public class EnemyInfo : ScriptableObject
    {
        [SerializeField] private string actorName = "none";
        [SerializeField] private Color color = Color.blue;
        [SerializeField] private float speed = 20f;
        [SerializeField] private float fireRate = 1f;
        [Tooltip("Distance from which the enemy starts firing")]
        [SerializeField] private float startFireDistance = 10f;

        public string Name => actorName;
        public Color Color => color;
        public float Speed => speed;
        public float FireRate => fireRate;
        public float StartFireDistance => startFireDistance;
    }
}