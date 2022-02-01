using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexBullet : EnemyController
{
    [SerializeField] private float timeToDestroy;
    private void OnEnable(){
        Invoke("CoolObject", timeToDestroy);
    }

    private void CoolObject(){
        PoolManager.Instance.CoolObject(this.gameObject, enemyType);
    }
}
