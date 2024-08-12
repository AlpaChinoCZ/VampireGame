using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace VG.UI
{
    /// <summary>
    /// Helper script to bind game manager to main menu - tricky way
    /// </summary>
    public class BindMenuHelper : MonoBehaviour
    {
        [SerializeField] private string playButtonName;
        [SerializeField] private string quitButtonName;

        public void Awake()
        {
            var play = GameObject.Find(playButtonName)?.GetComponent<Button>();
            var quit = GameObject.Find(quitButtonName)?.GetComponent<Button>();

            Assert.IsNotNull(play, $"{gameObject} play button is null");
            Assert.IsNotNull(quit, $"{gameObject} quit button is null");
            
            play.onClick.AddListener(delegate { VgGameManager.Instance.OpenLevel(1); });
            quit.onClick.AddListener(VgGameManager.Instance.QuitGame);
        }
    }
}