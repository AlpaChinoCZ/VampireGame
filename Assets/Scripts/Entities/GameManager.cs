using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace VG
{
    /// <summary>
    /// Abstract GameManager which holds all player important data, functions and components
    /// It will probably be a singleton that will persist between scenes
    /// </summary>
    public abstract class GameManager : MonoBehaviour
    {
        [Tooltip("Prefab which will be instantiate")]
        [SerializeField] private PlayerController playerControllerSpawnPrefab;
        [Tooltip("Prefab which will be instantiate")]
        [SerializeField] private Player playerSpawnPrefab;
        
        
        /// <summary>
        ///  Player prefab which will be instantiate on spawn
        /// </summary>
        public Player PlayerSpawnPrefab => playerSpawnPrefab;
        /// <summary>
        ///  PlayerController prefab which will be instantiate on spawn
        /// </summary>
        public PlayerController PlayerControllerSpawnPrefab => playerControllerSpawnPrefab;
        
        private PlayerController playerController;
        /// <summary>
        ///  PlayerController prefab in scene
        /// </summary>
        public PlayerController PlayerController
        { 
            get => playerController;
            protected set => playerController = value;
        }
        
        private Player player;
        /// <summary>
        ///  Player spawned in scene
        /// </summary>
        public Player Player 
        { 
            get => player;
            protected set => player = value;
        }
        
        protected virtual void Awake()
        {
            Assert.IsNotNull(playerControllerSpawnPrefab, $"{gameObject} has no Player Controller");
            Assert.IsNotNull(playerSpawnPrefab, $"{gameObject} has no Player Prefab");
        }
    }
}