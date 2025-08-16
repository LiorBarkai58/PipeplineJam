using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //References
    [SerializeField] private FishSwapManager fishSwapManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private List<Enemy> enemyList;

    [SerializeField] private CinemachineImpulseSource screenShakeSource;
    [SerializeField] private FishHealthManager fishHealthManager;
    //Vars
    private int score;
    private int level = 1;
    private List<GameObject> currentLevel;

    private void Awake()
    {
        currentLevel = levelManager.GenerateLevel(0);
        fishSwapManager.OnEndLevelEvent += OnEndLevel;
        foreach (var enemy in enemyList)
        {
            enemy.OnEnemyDeathEvent += AddScore;
            enemy.OnHitPlayerEvent += PlayerHit; //Need to write this function.
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public void PlayerHit()
    {
        screenShakeSource.GenerateImpulseWithForce(0.5f);
        fishHealthManager.TakeDamage();
    }

    public void OnEndLevel()
    {
        levelManager.DestroyLevel(currentLevel);
        level++;
        levelManager.GenerateLevel(level - 1);
    }
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        enemyList = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
    }
#endif
}
