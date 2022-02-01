using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeKiss : BasicBullet
{
    [SerializeField] private float rotationSpeedX;
    [SerializeField] private float rotationSpeedY;
    [SerializeField] private float rotationSpeedZ;

    // Update is called once per frame
    void Update(){
        this.gameObject.transform.Rotate(rotationSpeedX, rotationSpeedY, rotationSpeedZ);
    }
}
