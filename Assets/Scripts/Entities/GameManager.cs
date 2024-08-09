using UnityEngine;
using UnityEngine.Assertions;

namespace VG
{
    public abstract class GameManager : MonoBehaviour
    {
        [SerializeField] private UiManager uiManager;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Player player;
        
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();
                    if (instance == null)
                    {
                        instance = new GameObject().AddComponent<GameManager>();
                    }
                }

                return instance;
            }
        }
        public UiManager UiManager => uiManager;
        public PlayerController PlayerController => playerController;
        public Player Player => player;
        
        private static GameManager instance;
        
        protected virtual void Awake()
        {
            if (instance != null) Destroy(this);
            DontDestroyOnLoad(this);
            
            SetupManager();
            
            //Assert.IsNotNull(uiManager, $"{gameObject} has no UI Manager");
            Assert.IsNotNull(playerController, $"{gameObject} has no Player Controller");
            Assert.IsNotNull(player, $"{gameObject} has no Player");
        }

        private void SetupManager()
        {
            PlayerController.Init(player, Camera.main);
            
            Assert.IsNotNull(Camera.main, $"{gameObject} Main Player Camera is null.");
        }
    }
}