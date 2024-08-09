using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VG
{
    public class Health : MonoBehaviour, IDamageable, IHealth
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth = 100f;

        public event Action OnDamaged;
        public event Action OnHealthChanged;
        public event Action OnHealed;
        public event Action OnDead;
        public float MaxHealth => maxHealth;
        public float CurrentHealth
        {
            get => currentHealth;
            private set
            {
                if (value > MaxHealth || value < 0)
                {
                    currentHealth = value > MaxHealth ? MaxHealth : 0f;
                }
                else
                {
                    currentHealth = value;
                }
            }
        }
        
        public void Heal(float amount)
        {
            CurrentHealth += amount;
            OnHealthChanged?.Invoke();
            OnHealed?.Invoke();
        }
        
        public void ApplyDamage(float damage)
        {
            CurrentHealth -= damage;
            OnHealthChanged?.Invoke();
            OnDamaged?.Invoke();
            
            if (CurrentHealth <= 0f)
            {
                OnDead?.Invoke();
            }
        }

        private void Awake()
        {
            CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        }
    }
}
