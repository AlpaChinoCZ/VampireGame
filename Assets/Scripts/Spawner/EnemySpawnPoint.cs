using UnityEngine;
using UnityEngine.Assertions;

namespace VG
{
    public class EnemySpawnPoint : SpawnPoint
    {
        [SerializeField] private Enemy[] enemies;

        public Enemy[] Enemies => enemies;

        private void Awake()
        {
            Assert.IsTrue(enemies.Length > 0, $"{gameObject} have no enemies to spawn");
        }
    }
}
