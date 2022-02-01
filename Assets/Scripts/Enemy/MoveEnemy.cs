using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : EnemyController
{
    [SerializeField] private float movementSpeed;

    private Rigidbody2D rb;

    protected override void Awake(){
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move(){
        transform.position = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);

        Vector2 lookDir = (Vector2)target.position - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ManageCollisions(collision.gameObject);
    }

    protected virtual void ManageCollisions(GameObject col){
        if(col.CompareTag("Player")){
            TakeDamage(99);
        }
    }
}
