using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace VG
{
    public abstract class GameManager : MonoBehaviour
    {
        [Tooltip("Prefab which will be instantiate")]
        [SerializeField] private PlayerController playerControllerSpawnPrefab;
        [Tooltip("Prefab which will be instantiate")]
        [SerializeField] private Player playerSpawnPrefab;
        
        /// <summary>
        ///  PlayerController prefab in scene
        /// </summary>
        public PlayerController PlayerController
        { 
            get => playerController;
            protected set => playerController = value;
        }
        /// <summary>
        ///  PlayerController prefab which will be instantiate on spawn
        /// </summary>
        public PlayerController PlayerControllerSpawnPrefab => playerControllerSpawnPrefab;
        /// <summary>
        ///  Player spawned in scene
        /// </summary>
        public Player Player 
        { 
            get => player;
            protected set => player = value;
        }
        /// <summary>
        ///  Player prefab which will be instantiate on spawn
        /// </summary>
        public Player PlayerSpawnPrefab => playerSpawnPrefab;
        
        private PlayerController playerController;
        private Player player;
        
        protected virtual void Awake()
        {
            Assert.IsNotNull(playerControllerSpawnPrefab, $"{gameObject} has no Player Controller");
            Assert.IsNotNull(playerSpawnPrefab, $"{gameObject} has no Player Prefab");
        }
    }
}