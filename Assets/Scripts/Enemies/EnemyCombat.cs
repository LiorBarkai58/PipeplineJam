using System;
using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    public class EnemyCombat : MonoBehaviour
    {
        public event UnityAction<Collider2D> OnHitPlayerEvent;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Player))
            {
                OnHitPlayerEvent?.Invoke(other);
                
            }
        }
    }
}