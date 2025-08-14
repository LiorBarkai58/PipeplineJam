using UnityEngine;
using UnityEngine.Events;

public class PlatformEnemy : Enemy
{
    //References
    [SerializeField] private BoxCollider2D platformCollider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override Vector3 GetTarget()
    {
        return platformCollider.bounds.center;
    }
}
