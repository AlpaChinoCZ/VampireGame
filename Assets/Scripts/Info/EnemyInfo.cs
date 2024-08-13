using UnityEngine;

namespace VG
{
    [CreateAssetMenu(fileName = "EnemyInfo", menuName = "VG/Info/EnemyInfo")]
    public class EnemyInfo : ScriptableObject
    {
        [SerializeField] private string enemyName = "none";
        [SerializeField] private float speed = 20f;
        [SerializeField] private float fireRate = 1f;
        [Tooltip("Distance from which the enemy starts firing")]
        [SerializeField] private float startFireDistance = 10f;

        public string Name => enemyName;
        public float Speed => speed;
        public float FireRate => fireRate;
        public float StartFireDistance => startFireDistance;
    }
}