using UnityEngine;

namespace VG
{
    [DisallowMultipleComponent]
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private float spawnRadius = 1f;
        [SerializeField] private Color color = Color.green;

        public float SpawnRadius => spawnRadius;

        #if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, spawnRadius);
        }
        #endif
    }
}
