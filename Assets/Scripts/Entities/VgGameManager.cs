using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace VG
{
    public class VgGameManager : GameManager
    {
        public void OpenLevel(int index)
        {
            Assert.IsTrue(index <= SceneManager.sceneCountInBuildSettings - 1 && index > 0, $"{gameObject} index is out of Scene count");
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
            Application.Quit();
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
