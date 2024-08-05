using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VG
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth = 100f;
        
        public float MaxHealth => maxHealth;

        public float CurrentHealth
        {
            get => currentHealth;
            set
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
        
        public void ApplyDamage(float damage)
        {
            CurrentHealth -= damage;
        }

        private void Awake()
        {
            CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        }
    }
}
