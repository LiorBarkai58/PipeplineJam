using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player.FishWithLegs
{
    public class LegStomp : MonoBehaviour
    {
        public event UnityAction OnStomp;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.BreakableWall))
            {
                Destroy(other.gameObject);
                OnStomp?.Invoke();
            }
        }
    }
}