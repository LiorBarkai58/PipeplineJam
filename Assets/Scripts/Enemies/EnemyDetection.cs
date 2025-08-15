using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDetection : MonoBehaviour
{
    //Events
    public event UnityAction<Collider2D> OnTriggerEnterEvent;
    public event UnityAction<Collider2D> OnTriggerExitEvent;
    
    //References
    [SerializeField] private CircleCollider2D detectionRange;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnTriggerEnterEvent.Invoke(other);
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnTriggerEnterEvent.Invoke(other);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnTriggerExitEvent.Invoke(detectionRange);
        }
    }
}
