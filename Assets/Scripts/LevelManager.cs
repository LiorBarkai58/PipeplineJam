using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //References
    [SerializeField] private GameObject levelSegmentPrefab;
    
    //Vars
    [SerializeField] private int levelSegmentCount;
    [SerializeField] private float gap = 20.48f;
    [SerializeField] private int bgCount = 2;

    private void GenerateLevel()
    {
        for (int i = 0; i < levelSegmentCount; i++)
        {
            Instantiate(levelSegmentPrefab, new Vector3(transform.position.x, transform.position.y + (-gap * 2 * bgCount), 0), transform.rotation);
        }
    }
}
