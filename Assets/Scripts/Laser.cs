using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask layerMask;

    Vector3 tmp = new Vector3(1,1,0);
    void Update(){
        lineRenderer.SetPosition(0, transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, tmp, layerMask);
        lineRenderer.SetPosition(1, transform.position * 100);

        // if(hit.collider){
        //     Vector3 finalLine = new Vector3(hit.point.x, hit.point.y, 0);
        //     lineRenderer.SetPosition(1, finalLine);
        // }else{
        //     //transform.up * 2000
        //     lineRenderer.SetPosition(1, tmp * 2000);
        // }
    }
}
