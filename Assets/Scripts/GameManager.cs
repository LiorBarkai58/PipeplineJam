using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //References
    [SerializeField] private List<Enemy> enemyList;
    
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
        
    }
}
