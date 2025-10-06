using UnityEngine;

namespace VG
{
    [RequireComponent(typeof(Rigidbody))]
    public class HealPickup : Pickup
    {
        [SerializeField] private float healAmount = 10f;
        
        public float HealAmount => healAmount;

        protected virtual void OnTriggerEnter(Collider other)
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.Health.Heal(HealAmount);
                Destroy(gameObject);
            }
        }
    }
}
