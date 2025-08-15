using System;
using UnityEngine;
using UnityEngine.Events;

public class PlatformEnemy : Enemy
{
    //References
    [SerializeField] private Transform platform;
    
    //Vars
    [SerializeField] private float direction = 1;
    
    private Vector3[] corners = new Vector3[4];
    private int cornerIndex;

    private void Awake()
    {
        Vector3 halfSize = platform.lossyScale / 2f;
        Vector3 center = platform.position;
        
        corners[0] = center + new Vector3(halfSize.x, halfSize.y, 0);  // Top right
        corners[1] = center + new Vector3(halfSize.x, -halfSize.y, 0); // Bottom right
        corners[2] = center + new Vector3(-halfSize.x, -halfSize.y, 0);// Bottom left
        corners[3] = center + new Vector3(-halfSize.x, halfSize.y, 0); // Top left
        target = GetTarget();
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
            cornerIndex++;
            if (cornerIndex >= corners.Length)
                cornerIndex = 0;
            target = GetTarget();
            this.transform.rotation = Quaternion.Euler(0,0,90 *  cornerIndex * -direction);
        }
    }

    public override Vector3 GetTarget()
    {
        return corners[cornerIndex];
    }
}
