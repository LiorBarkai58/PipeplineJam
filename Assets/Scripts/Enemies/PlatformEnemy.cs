using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PlatformEnemy : Enemy
{
    //References
    [SerializeField] private Collider2D platform;
    
    //Vars
    [SerializeField] private float direction = 1;
    
    private Vector3[] corners = new Vector3[4];
    private int cornerIndex;

    private void Awake()
    {
        // Vector3 halfSize = platform.lossyScale / 2f;
        // Vector3 center = platform.position;
        //
        // corners[0] = center + new Vector3(halfSize.x, halfSize.y, 0);  // Top right
        // corners[1] = center + new Vector3(halfSize.x, -halfSize.y, 0); // Bottom right
        // corners[2] = center + new Vector3(-halfSize.x, -halfSize.y, 0);// Bottom left
        // corners[3] = center + new Vector3(-halfSize.x, halfSize.y, 0); // Top left
        
        corners[0] = new Vector3(platform.bounds.center.x + platform.bounds.extents.x, platform.bounds.max.y, 0);
        corners[1] = new Vector3(platform.bounds.center.x + platform.bounds.extents.x, platform.bounds.min.y, 0);
        corners[2] = new Vector3(platform.bounds.center.x - platform.bounds.extents.x, platform.bounds.min.y, 0);
        corners[3] = new Vector3(platform.bounds.center.x - platform.bounds.extents.x, platform.bounds.max.y, 0);
        target = GetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(!platform) Destroy(gameObject);
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
            this.transform.DORotateQuaternion(Quaternion.Euler(0,0,90 *  cornerIndex * -direction), 0.5f).SetEase(Ease.OutQuad);
        }
    }

    public override Vector3 GetTarget()
    {
        return corners[cornerIndex];
    }
}
