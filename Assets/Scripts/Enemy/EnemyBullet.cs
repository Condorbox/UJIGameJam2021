using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MoveEnemy
{
    protected override void ManageCollisions(GameObject col)
    {
        TakeDamage(99);
    }
}
