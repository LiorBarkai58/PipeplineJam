using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    //References
    [SerializeField] private List<GameObject> levelSegmentPrefabs;
    [SerializeField] private GameObject endLevelSegmentPrefab;
    
    //Vars
    [SerializeField] private float gap = 20.48f;
    [SerializeField] private int bgCount = 2;

    private void Awake()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        for (int i = 0; i < levelSegmentPrefabs.Count; i++)
        {
            Instantiate(levelSegmentPrefabs[i], new Vector3(transform.position.x, transform.position.y + (-gap* bgCount* i), 0), transform.rotation);
        }
        Instantiate(endLevelSegmentPrefab, new Vector3(transform.position.x, transform.position.y + (-gap * bgCount* levelSegmentPrefabs.Count), 0), transform.rotation);
    }

    private void GenerateLevelRandom()
    {
        List<GameObject> copy = new List<GameObject>(levelSegmentPrefabs);
        for (int i = 0; i < levelSegmentPrefabs.Count; i++)
        {
            int selected = Random.Range(0, copy.Count);
            GameObject temp = copy[selected];
            Instantiate(temp,new Vector3(transform.position.x, transform.position.y + (-gap * bgCount * i), 0), transform.rotation);
            copy.Remove(temp);
        }
        Instantiate(endLevelSegmentPrefab, new Vector3(transform.position.x, transform.position.y + (-gap * bgCount* (levelSegmentPrefabs.Count + 1)), 0), transform.rotation);
    }
}
