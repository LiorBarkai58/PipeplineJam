using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "Scriptable Objects/LevelScriptableObject")]
public class LevelScriptableObject : ScriptableObject
{
    public List<GameObject> LevelSegmentsPrefabs = new List<GameObject>();
    public GameObject LevelEndPrefab;
}
