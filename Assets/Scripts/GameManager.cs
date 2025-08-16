using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //References
    [SerializeField] private List<Enemy> enemyList;

    [SerializeField] private CinemachineImpulseSource screenShakeSource;
    [SerializeField] private FishHealthManager fishHealthManager;
    //Vars
    private int score;

    private void Awake()
    {
        foreach (var enemy in enemyList)
        {
            enemy.OnEnemyDeathEvent += AddScore;
            enemy.OnHitPlayerEvent += PlayerHit; //Need to write this function.
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        enemyList = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
    }
#endif
}
