using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class FishHealthManager : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 3;

        private int currentHealth;
        
        public event UnityAction OnDeathEvent;
        private void Start()
        {
            currentHealth = maxHealth;
        }

        private void HandleTakeDamage()
        {
            currentHealth--;
            //screenshake
            if (currentHealth <= 0)
            {
                OnDeathEvent?.Invoke();
            }
        }
        public void TakeDamage()
        {
            HandleTakeDamage();
        }
    }
}