using System;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Player.FishWithLegs
{
    public class LegStomp : MonoBehaviour
    {
        public event UnityAction OnStomp;
        
        [SerializeField] private float minAirTime = 0.2f;
        
        private StopwatchTimer _airTimer = new StopwatchTimer();

        public bool CanBreak = false;
        private void OnEnable()
        {
            _airTimer?.Start();
        }

        private void OnDisable()
        {
            _airTimer?.Stop();
            _airTimer?.Reset();
            
        }

        private void Update()
        {
            _airTimer?.Tick(Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"Passed: {_airTimer.TimePassed}");
            if (!other.CompareTag(Tags.BreakableWall) || !CanBreak) return;
            Destroy(other.gameObject);
            OnStomp?.Invoke();
        }
    }
}