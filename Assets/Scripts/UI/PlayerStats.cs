using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace VG.UI
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Text;
        private Dictionary<string, int> killCounter;
        
        private void Start()
        {
            StartCoroutine(WaitCoroutine());
        }

        private IEnumerator WaitCoroutine()
        {
            yield return new WaitUntil(() => killCounter != null);
            
            UpdateText();
        }
        
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
            StopAllCoroutines();
        }

        private void AddListeners()
        {
            VgGameManager.Instance.OnInitCounter += InitCounter;
            VgGameManager.Instance.OnUpdateCounter += UpdateText;
        }

        private void RemoveListeners()
        {
            VgGameManager.Instance.OnInitCounter -= InitCounter;
            VgGameManager.Instance.OnUpdateCounter -= UpdateText;
        }
        
        private void InitCounter(Dictionary<string, int> counter)
        {
            killCounter = counter;
        }
        
        private void UpdateText()
        {
            var statsText = "";
            foreach (var item in killCounter)
            {
                if(item.Value != 0) statsText += $"{item.Key} : {item.Value}\n";
            }

            Text.text = statsText;
        }
    }
}
