using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    //Events
    public event UnityAction OnHitPlayerEvent; 
    
    //References
    [SerializeField] private EnemyDetection detector;
    [SerializeField] private BoxCollider2D collider;
    
    //Vars
    [SerializeField] private float targetMinDistance;
    [SerializeField] private float targetMaxDistance;
    [SerializeField] private float speed;
    [SerializeField] private float detectionRangeOffset = 0.1f;
    
    private Vector3 target;
    
    private void Awake()
    {
        detector.OnTriggerEnterEvent += TargetPlayer;
        detector.OnTriggerExitEvent += FindTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position != target)
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

    private Vector3 GetTarget()
    {
        float radius = Random.Range(targetMinDistance, targetMaxDistance);
        float angle = Random.Range(0, Mathf.PI * 2);
        Vector3 direction = detector.transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        return direction;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (collider.bounds.max.y - detectionRangeOffset > other.collider.bounds.min.y)
            {
                OnHitPlayerEvent?.Invoke();
                Debug.Log("Hit Player");
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}



