using UnityEngine;
using UnityEngine.Assertions;

namespace VG
{
    [RequireComponent(typeof(Rigidbody))]
    public class Enemy : Actor
    {
        [SerializeField] private EnemyInfo enemyInfo;
        [SerializeField] private BasicFire fireComponent;
        [SerializeField] private EnemyController enemyController;
        
        public EnemyInfo Info => enemyInfo;
        public BasicFire FireComponent => fireComponent;
        public EnemyController EnemyController => enemyController;
        
        private Rigidbody body;
        
        protected override void Awake()
        {
            base.Awake();

            body = GetComponent<Rigidbody>();
            
            Assert.IsNotNull(enemyInfo, $"{gameObject} Enemy Info is null");
            Assert.IsNotNull(fireComponent, $"{gameObject} Fire Component is null");
        }
        
        public void Died()
        {
            VgGameManager.Instance.UpdateCounter(this);
            VgGameManager.Instance.Player.RemoveNearestObject(transform);
            Destroy(gameObject);
        }
    }
}
