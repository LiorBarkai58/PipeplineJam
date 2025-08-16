using System;
using Unity.VisualScripting;
using UnityEngine;

public class WallEnemy : Enemy
{
    private static readonly int Attack = Animator.StringToHash("Attack");

    //References
    [SerializeField] private EnemyDetection detector;

    [SerializeField] private Animator animator;
    //Vars
    [SerializeField] private float distance;

    private int direction = 1;
    private bool _isAttacking = false;

    private void Awake()
    {
        detector.OnTriggerEnterEvent += OnPlayerInRange;
        target = GetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAttacking) return;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
        {
            target = GetTarget();
        }
    }

    public override Vector3 GetTarget()
    {
        float newY = transform.position.y + distance * direction;
        direction *= -1;
        transform.localScale = new Vector3(1, -direction, 1);
        return new Vector3(transform.position.x ,newY, transform.position.z);
    }

    public void OnPlayerInRange(Collider2D player)
    {
        Debug.Log("Crab Attack");
        animator.SetTrigger(Attack);
        _isAttacking = true;
    }

    public void FinishAttack()
    {
        _isAttacking = false;
    }
    
}
