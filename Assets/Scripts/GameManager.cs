using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using ScenesHelpers;
using ScenesHelpers.Editor;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //References
    [SerializeField] private FishSwapManager fishSwapManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private List<Enemy> enemyList;

    [SerializeField] private FishHealthManager fishHealthManager;

    [SerializeField] private SceneRequestChannel sceneRequestChannel;
    
    [SerializeField] private SceneField tyforPlaying;
    //Vars
    private int score;
    private int level = 1;
    private List<GameObject> currentLevel;

    private void Awake()
    {
        currentLevel = levelManager.GenerateLevel(0);
        fishSwapManager.OnEndLevelEvent += OnEndLevel;
        fishHealthManager.OnDeathEvent += PlayerDeath;                               
        uiManager.UpdateLevel(1);

        enemyList = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
        
        foreach (var enemy in enemyList)
        {
            enemy.OnEnemyDeathEvent += AddScore;
            enemy.OnHitPlayerEvent += PlayerHit; //Need to write this function.
        }
    }

    private void OnDestroy()
    {
        fishSwapManager.OnEndLevelEvent -= OnEndLevel;
        fishHealthManager.OnDeathEvent -= PlayerDeath; 
    }

    public void AddScore(int score)
    {
        this.score += score;
        uiManager.UpdateScore(this.score);
    }

    public void PlayerHit()
    {
        uiManager.UpdateHealth(fishHealthManager.TakeDamage());
    }

    public void PlayerDeath()
    {
        sceneRequestChannel.RequestSceneChange(tyforPlaying);
        // uiManager.UpdateHealthHeal(fishHealthManager.MaxHealth);
    }

    public void OnEndLevel()
    {
        levelManager.DestroyLevel(currentLevel);
        level++;
        levelManager.GenerateLevel(level - 1);
        
        enemyList = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();

        uiManager.UpdateLevel(level);
        uiManager.UpdateHealth(fishHealthManager.MaxHealth);
    }
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        enemyList = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
    }
#endif
}
