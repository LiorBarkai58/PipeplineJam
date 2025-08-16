using System;
using Enemies;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy: MonoBehaviour
{
    //Events
    public event UnityAction OnHitPlayerEvent; 
    public event UnityAction<int> OnEnemyDeathEvent;
    
    //References
    [SerializeField] protected BoxCollider2D collider;

    [SerializeField] private EnemyCombat combat;
    //Vars
    [SerializeField] private float detectionRangeOffset = 0.1f;
    [SerializeField] protected float speed;
    [SerializeField] protected int scoreGain;
    
    protected Vector3 target;

    public abstract Vector3 GetTarget();

    private void OnEnable()
    {
        combat.OnHitPlayerEvent += HandlePlayerHit;
    }

    private void OnDisable()
    {
        combat.OnHitPlayerEvent -= HandlePlayerHit;
    }

    private void HandlePlayerHit(Collider2D other)
    {
        OnHitPlayerEvent?.Invoke();
        
    }

    private void OnDestroy()
    {
        OnEnemyDeathEvent?.Invoke(scoreGain);
    }
}
