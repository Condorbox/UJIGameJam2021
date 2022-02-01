using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected PoolObjectType enemyType;
    [SerializeField] protected float maxHealth;
    protected float health;
    [SerializeField] protected GameObject deathVFX;
    [Space(10)]
    [SerializeField] private bool immortal = false;
    [Space(10)]
    [SerializeField] protected Transform target;
    [SerializeField] private int score;
    [SerializeField] private int money;
    [SerializeField] private int soul;
    [SerializeField] private GameData gameData;
    protected virtual void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if(!immortal){
            health -= damage;
            Die();
        }
    }

    protected virtual void Die()
    {
        if (health <= 0)
        {
            if (deathVFX != null) { Instantiate(deathVFX, transform.position, deathVFX.transform.rotation); }
            gameData.score += score;
            gameData.money += money;
            gameData.soulsToCollect += soul;
            health = maxHealth;
            PoolManager.Instance.CoolObject(this.gameObject, enemyType);
        }
    }

    public void SetTarget(Transform target){
        this.target = target;
    }

    public PoolObjectType GetEnemyType(){
        return enemyType;
    }
}
