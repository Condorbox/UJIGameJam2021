using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexEnemy : EnemyController
{
    [SerializeField] Transform[] positions;
    [SerializeField] float[] timeBtwAttack = new float[2];
    float counter;
    [SerializeField] EnemyController bullet;
    [SerializeField] float velocity;

    private void OnEnable() {
        counter = Random.Range(timeBtwAttack[0], timeBtwAttack[1]);
    }

    private void Update(){
        counter -= Time.deltaTime;
        if(counter <= 0){
            Attack();
            counter = Random.Range(timeBtwAttack[0], timeBtwAttack[1]);
        }
    }

    private void Attack(){
        for(int i = 0; i < positions.Length; i++){
            PoolObjectType poolObjectType = bullet.GetEnemyType();

            GameObject newBullet = PoolManager.Instance.GetPoolObject(poolObjectType);
            newBullet.transform.position = positions[i].position;
            newBullet.transform.rotation = positions[i].rotation;
            newBullet.gameObject.SetActive(true);

            Rigidbody2D rbBullet = newBullet.GetComponent<Rigidbody2D>();
            rbBullet.AddForce(positions[i].up  * velocity, ForceMode2D.Impulse);
        }
    }

}
