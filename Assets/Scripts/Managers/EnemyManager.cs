using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance; public static EnemyManager inst { get { return instance; } }

    [SerializeField] private List<EnemyEntity> enemiesOnMap = new List<EnemyEntity>(); public List<EnemyEntity> Enemies { get { return enemiesOnMap; } }
    public Action<EnemyEntity> removeEnemy;

    void Awake() {
        instance = this;
        removeEnemy += RemoveEnemy;
    }

    public void RegisterEnemy(EnemyEntity enemy) {
        Enemies.Add(enemy);
    }

    public void InstantiateEnemies(List<EnemyEntity> newEnemies) {
        enemiesOnMap.Clear();
        enemiesOnMap = new List<EnemyEntity>(newEnemies);
    }

    public void RemoveEnemy(EnemyEntity enemy) {
        enemiesOnMap.Remove(enemy);
    }
}
