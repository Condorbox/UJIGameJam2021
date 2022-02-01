using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : EnemyController
{
    [Header("SpawnEnemy")]
    [SerializeField] private EnemyController bullet;
    [SerializeField] private float timeBtwAttack;
    private float counter;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Rigidbody2D rb;

    void Update()
    {
        counter -= Time.deltaTime;
        if(counter <= 0){
            Spawn();
            counter = timeBtwAttack;
        }
    }

    void FixedUpdate(){
        Vector2 lookDir = target.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
    private void Spawn(){
        PoolObjectType poolObjectType = bullet.GetEnemyType();
        GameObject newbullet = PoolManager.Instance.GetPoolObject(poolObjectType);
        newbullet.transform.position = shootPoint.transform.position;
        newbullet.GetComponent<EnemyController>().SetTarget(target);
        newbullet.gameObject.SetActive(true);
    }
}
