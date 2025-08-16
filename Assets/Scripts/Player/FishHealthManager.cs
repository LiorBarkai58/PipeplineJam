using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class FishHealthManager : MonoBehaviour
    {
        public int MaxHealth {
            get { return maxHealth; }
        }

        [SerializeField] private int maxHealth = 2;

        private int currentHealth;
        
        public event UnityAction OnDeathEvent;
        private void Start()
        {
            currentHealth = maxHealth;
        }

        private void HandleTakeDamage()
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                OnDeathEvent?.Invoke();
            }
        }
        public int TakeDamage()
        {
            HandleTakeDamage();
            return currentHealth;
        }

        public void ResetHp()
        {
            currentHealth = maxHealth;
        }
    }
}