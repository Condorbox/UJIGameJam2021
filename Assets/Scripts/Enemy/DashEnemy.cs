using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : EnemyController
{
    [Header("DashEnemy")]
    private DashEnemyEnum state;
    [SerializeField] private float idleSpeed;
    [SerializeField] private float attackSpeed;

    [SerializeField] private float[] timeBtwAttack = new float[2];
    [SerializeField] private float[] attackDuration = new float[2];
    [SerializeField] private float[] timeToRest = new float[2];
    private float counter;

    [SerializeField] private Rigidbody2D rb;
    private Vector3 attackPos;
    [SerializeField] private ParticleSystem ps;

    void Start(){
        state = DashEnemyEnum.Idle;
        counter = Random.Range(timeBtwAttack[0], timeBtwAttack[1]);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state){
            case DashEnemyEnum.Idle:
                Idle();
                break;
            case DashEnemyEnum.Attack:
                Attack();
                break;
            case DashEnemyEnum.Rest:
                Rest();
                break;
        }
    }

    // void FixedUpdate(){
    //     if(state != DashEnemyEnum.Rest){
    //         Vector2 lookDir = (Vector2)target.position - rb.position;
    //         float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
    //         rb.rotation = angle;
    //     }
    // }

    private void Idle(){
        Move(target.position, idleSpeed);

        counter -= Time.deltaTime;
        if(counter<=0){
            ps.Play();
            counter = Random.Range(attackDuration[0], attackDuration[1]);
            attackPos = target.position;
            state = DashEnemyEnum.Attack;
        }
    }
    private void Attack(){
        Move(attackPos, attackSpeed);

        counter -= Time.deltaTime;
        if(counter <= 0){
            ps.Stop();
            counter = Random.Range(timeToRest[0], timeToRest[1]);
            state = DashEnemyEnum.Rest;
        }
    }
    private void Rest(){
        counter -= Time.deltaTime;
        if(counter <= 0){
            counter = Random.Range(timeBtwAttack[0], timeBtwAttack[1]);
            state = DashEnemyEnum.Idle;
        }
    }

    private void Move(Vector3 pos, float movementSpeed){
        transform.position = Vector3.MoveTowards(transform.position, pos, movementSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected() {
        if(state == DashEnemyEnum.Idle) Gizmos.color = Color.green;
        else if(state == DashEnemyEnum.Attack)  Gizmos.color = Color.red;
        else Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position, target.position);
    }
}

public enum DashEnemyEnum{
    Idle,
    Attack,
    Rest
}
