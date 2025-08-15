using System;
using UnityEngine;

public class WallEnemy : Enemy
{
    //Vars
    [SerializeField] private float distance;

    private int direction = 1;

    private void Awake()
    {
        target = GetTarget();
    }

    // Update is called once per frame
    void Update()
    {
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
        transform.localScale = new Vector3(1, direction, 1);
        return new Vector3(transform.position.x ,newY, transform.position.z);
    }
}
