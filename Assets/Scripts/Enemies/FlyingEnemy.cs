using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class FlyingEnemy : Enemy
{
    //References
    [SerializeField] private EnemyDetection detector;
    
    //Vars
    [SerializeField] private float targetMinDistance;
    [SerializeField] private float targetMaxDistance;
    
    
    private void Awake()
    {
        detector.OnTriggerEnterEvent += TargetPlayer;
        detector.OnTriggerExitEvent += FindTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            target = GetTarget();
        }
    }

    private void TargetPlayer(Collider2D player)
    {
        target = player.transform.position;
        Debug.Log("Targets Player");
    }

    private void FindTarget(Collider2D radius)
    {
        target = GetTarget();
        Debug.Log("Targets Point");
    }

    public override Vector3 GetTarget()
    {
        float radius = Random.Range(targetMinDistance, targetMaxDistance);
        float angle = Random.Range(0, Mathf.PI * 2);
        Vector3 direction = detector.transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        return direction;
    }
}



