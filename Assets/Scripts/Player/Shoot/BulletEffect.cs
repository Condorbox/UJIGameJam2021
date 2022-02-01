using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    private void OnEnable(){
        Invoke("CoolObject", timeToDestroy);
    }

    private void CoolObject(){
        PoolManager.Instance.CoolObject(this.gameObject, PoolObjectType.BulletEffect);
    }
}
