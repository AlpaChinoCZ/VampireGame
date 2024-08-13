using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace VG
{
    /// <summary>
    /// Info that is used when spawning, especially after an event
    /// </summary>
    [CreateAssetMenu(fileName = "SpawnPickupInfo", menuName = "VG/Info/SpawnPickupInfo")]
    public class SpawnPickupInfo : ScriptableObject
    {
        [SerializeField] private float spawnProbability = 0.5f;
        [SerializeField] private Vector2 randomRadius = new Vector2(0f,1f);
        [SerializeField] private Pickup[] pickups;
        
        public void Spawn(Transform transform)
        {
            Validate();
            foreach (var pickup in pickups)
            {
                var randomValue = Random.Range(0f, 1f);

                if (randomValue <= spawnProbability)
                {
                    var newPos = transform.position;
                    newPos.x = Random.Range(randomRadius.x, randomRadius.y);
                    newPos.z = Random.Range(randomRadius.x, randomRadius.y);
                    Instantiate(pickup, transform.position, transform.rotation);
                }
            }
        }

        public void Validate()
        {
            Assert.IsTrue(pickups.Length > 0, "Pickups are empty");
            Assert.IsTrue(spawnProbability is <= 1 and >= 0, "Spawn probability is not in range (0-1)");
        }
    }
}
