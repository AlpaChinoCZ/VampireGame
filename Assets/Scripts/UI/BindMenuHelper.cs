using UnityEngine;
using UnityEngine.Assertions;
using Button = UnityEngine.UI.Button;

namespace VG.UI
{
    /// <summary>
    /// Helper script to bind game manager to main menu - tricky way
    /// </summary>
    public class BindMenuHelper : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        public void Awake()
        {   
            Assert.IsNotNull(playButton, $"{gameObject} play button is null");
            Assert.IsNotNull(playButton, $"{gameObject} quit button is null");
            
            playButton.onClick.AddListener(delegate { VgGameManager.Instance.OpenLevel(1); });
            quitButton.onClick.AddListener(VgGameManager.Instance.QuitGame);
        }
    }
}