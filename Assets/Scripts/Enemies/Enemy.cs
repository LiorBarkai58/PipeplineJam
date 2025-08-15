using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy: MonoBehaviour
{
    //Events
    public event UnityAction OnHitPlayerEvent; 
    public event UnityAction<int> OnEnemyDeathEvent;
    
    //References
    [SerializeField] protected BoxCollider2D collider;
    
    //Vars
    [SerializeField] protected float detectionRangeOffset = 0.1f;
    [SerializeField] protected float speed;
    [SerializeField] protected int scoreGain;
    
    protected Vector3 target;

    public abstract Vector3 GetTarget();
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (collider.bounds.max.y - detectionRangeOffset > other.collider.bounds.min.y)
            {
                OnHitPlayerEvent?.Invoke();
                Debug.Log("Hit Player");
            }
            
        }
    }

    private void OnDestroy()
    {
        OnEnemyDeathEvent?.Invoke(scoreGain);
        
    }
}
