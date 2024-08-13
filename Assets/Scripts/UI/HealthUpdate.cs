using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace VG.UI
{
    public class HealthUpdate : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;
        
        private void Start()
        {
            Assert.IsNotNull(healthText, $"{gameObject} text is null");
            Assert.IsNotNull(VgGameManager.Instance.Player, $"{gameObject} player is null");

            SetHealthText(VgGameManager.Instance.Player.Health.CurrentHealth, VgGameManager.Instance.Player.Health.MaxHealth);
        }

        public void UpdateText()
        {
            SetHealthText(VgGameManager.Instance.Player.Health.CurrentHealth, VgGameManager.Instance.Player.Health.MaxHealth);
        }
        
        public void SetHealthText(float currentHealth, float maxHealth)
        {
            healthText.text = $"{currentHealth} / {maxHealth}";
        }
    }
}
