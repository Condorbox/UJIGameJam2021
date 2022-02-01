using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private EnemyController enemyToSpawn;
    [SerializeField] private float[] timeToSpawn = new float[2];
    private float counter;
    private Transform target;

    void Start()
    {
        counter = Random.Range(timeToSpawn[0], timeToSpawn[1]);
    }

    void Update()
    {
        counter -= Time.deltaTime;
        if(counter <= 0){
            Spawn();
            counter = Random.Range(timeToSpawn[0], timeToSpawn[1]);
            PoolManager.Instance.CoolObject(this.gameObject, PoolObjectType.Spawner);
        }
    }

    private void Spawn(){
        PoolObjectType poolObjectType = enemyToSpawn.GetEnemyType();
        GameObject newEnemy = PoolManager.Instance.GetPoolObject(poolObjectType);
        newEnemy.transform.position = transform.position;
        newEnemy.GetComponent<EnemyController>().SetTarget(target);
        newEnemy.gameObject.SetActive(true);
    }

    public void SetEnemyToSpawn(EnemyController enemyToSpawn){
        this.enemyToSpawn = enemyToSpawn;
    }

    public void SetTarget(Transform target){
        this.target = target;
    }
}
