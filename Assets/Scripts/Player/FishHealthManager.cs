using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class FishHealthManager : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 3;

        private int currentHealth;
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
                //handle death
            }
        }
        public void TakeDamage()
        {
            HandleTakeDamage();
        }
    }
}