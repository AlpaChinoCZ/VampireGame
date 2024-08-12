using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace VG
{
    /// <summary>
    /// Singleton GameManager
    /// </summary>
    public class VgGameManager : GameManager
    {
        public event Action OnUpdateCounter;
        public event Action<Dictionary<string, int>> OnInitCounter;
        
        public Dictionary<string, int> KillCounter => killCounter;
        public static VgGameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<VgGameManager>();
                    if (instance == null)
                    {
                        instance = new GameObject().AddComponent<VgGameManager>();
                    }
                }

                return instance;
            }
        }
        
        private Dictionary<string, int> killCounter;
        private static VgGameManager instance;

        public void InitKillCounter(Enemy[] enemies)
        {
            killCounter ??= new Dictionary<string, int>();
            killCounter.Clear();
            foreach (var enemy in enemies)
            {
                if (!killCounter.ContainsKey(enemy.Info.Name))
                    killCounter.TryAdd(enemy.Info.Name, 0);
            }
            OnInitCounter?.Invoke(KillCounter);
        }

        public void UpdateCounter(Enemy enemy)
        {
            KillCounter[enemy.Info.Name]++;
            OnUpdateCounter?.Invoke();
        }

        public void OnPlayerDied()
        {
            PlayerController.enabled = false;
            PlayerController.SwitchActionMap(PlayerController.ActionMap.UI);
            OpenLevel((int)SceneType.MainMenu);
        }
        
        public void OpenLevel(int index)
        {
            Assert.IsTrue(index <= (SceneManager.sceneCountInBuildSettings - 1) && index >= 0, $"{gameObject} index is out of Scene count");
            SceneManager.LoadScene(index);
            if (index == (int)SceneType.MainMenu)
            {
                PlayerController.enabled = false;
                PlayerController.SwitchActionMap(PlayerController.ActionMap.UI);
            }
            else
            {
                PlayerController.enabled = true;
                PlayerController.SwitchActionMap(PlayerController.ActionMap.Player);
            }
        }
        
        public Player SpawnPlayer()
        {
            var scenePlayer = FindObjectOfType<Player>();
            if (scenePlayer == null)
            {
                var spawnPoint = GetPlayerSpawnPoint();
                
                if (spawnPoint != null)
                {
                    var spawnedPlayer = Instantiate(PlayerSpawnPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    Player = spawnedPlayer;
                    return Player;
                }
            
                Assert.IsNotNull(spawnPoint, $"{gameObject} player spawn point was not found");
                return null;
            }

            Player = scenePlayer;
            return Player;
        }
        
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        protected override void Awake()
        {
            base.Awake();
            
            if (instance != null && instance != this) Destroy(this);
            else instance = this; 
            
            DontDestroyOnLoad(this);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void Start()
        {
            PlayerController = Instantiate(PlayerControllerSpawnPrefab);
            PlayerController.InitInput();
            PlayerController.SwitchActionMap(PlayerController.ActionMap.UI);
            PlayerController.enabled = false;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == (int)SceneType.MainMenu)
            {
                Player = null;
            }
            else
            {
                SpawnPlayer();
                PlayerController.Init(Player, Camera.main);
            }
        }

        /// <summary>
        /// Find and return spawn point with Player tag
        /// </summary>
        private SpawnPoint GetPlayerSpawnPoint()
        {
            var points = FindObjectsOfType<SpawnPoint>();
            foreach (var point in points)
            {
                if (point.CompareTag("Player"))
                {
                    return point;
                }
            }
            return null;
        }
        
        public enum SceneType
        {
            MainMenu = 0,
            Level1 = 1
        }
    }
}