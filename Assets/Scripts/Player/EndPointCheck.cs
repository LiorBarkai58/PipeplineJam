using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class EndPointCheck : MonoBehaviour
    {
        
        public event UnityAction OnEndPointReached;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.EndPoint))
            {
                OnEndPointReached?.Invoke();
            }
        }
    }
}