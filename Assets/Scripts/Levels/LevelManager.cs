using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    //References
    [SerializeField] private List<LevelScriptableObject> levels;
    
    //Vars
    [SerializeField] private float gap = 20.0f;
    [SerializeField] private int bgCount = 2;
    
    public List<GameObject> GenerateLevel(int level)
    {
        if (levels.Count == level)
        {
            //Move to end game UI screen
        }
        List<GameObject> currentLevel = new List<GameObject>();
        for (int i = 0; i < levels[level].LevelSegmentsPrefabs.Count; i++)
        {
            currentLevel.Add(Instantiate(levels[level].LevelSegmentsPrefabs[i], new Vector3(transform.position.x, transform.position.y + (-gap* bgCount* i), 0), transform.rotation));
        }
        currentLevel.Add(Instantiate(levels[level].LevelEndPrefab, new Vector3(transform.position.x, transform.position.y + (-gap * bgCount* levels[level].LevelSegmentsPrefabs.Count), 0), transform.rotation));
        return currentLevel;
    }

    public void DestroyLevel(List<GameObject> levelList)
    {
        foreach (var level in levelList)
        {
            Destroy(level);
        }
    }

    public void GenerateLevelRandom(int level)
    {
        List<GameObject> copy = new List<GameObject>(levels[level].LevelSegmentsPrefabs);
        for (int i = 0; i < levels[level].LevelSegmentsPrefabs.Count; i++)
        {
            int selected = Random.Range(0, copy.Count);
            GameObject temp = copy[selected];
            Instantiate(temp,new Vector3(transform.position.x, transform.position.y + (-gap * bgCount * i), 0), transform.rotation);
            copy.Remove(temp);
        }
        Instantiate(levels[level].LevelEndPrefab, new Vector3(transform.position.x, transform.position.y + (-gap * bgCount* (levels[level].LevelSegmentsPrefabs.Count + 1)), 0), transform.rotation);
    }
}
