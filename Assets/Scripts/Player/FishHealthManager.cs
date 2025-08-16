using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Audio;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class FishHealthManager : MonoBehaviour
    {
        [SerializeField] private CinemachineImpulseSource screenShakeSource;
        [SerializeField] private AudioClip hurtClip;
        public int MaxHealth {
            get { return maxHealth; }
        }

        private bool _iFrame = false;

        [SerializeField] private int maxHealth = 2;

        private int currentHealth;
        
        public event UnityAction OnDeathEvent;
        private void Start()
        {
            currentHealth = maxHealth;
        }

        private void HandleTakeDamage()
        {
            if (_iFrame) return;
            currentHealth--;
            StartCoroutine(IFrame());
            screenShakeSource.GenerateImpulseWithForce(2f);
            SfxManager.Instance?.PlaySFX(hurtClip, 1f);
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

        private IEnumerator IFrame()
        {
            _iFrame = true;
            yield return new WaitForSeconds(1f);
            _iFrame = false;
        }
    }
}