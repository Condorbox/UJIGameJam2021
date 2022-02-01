using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask enemylayer;
    [SerializeField] private LayerMask layerToDestroy;
    [SerializeField] private PoolObjectType type;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private GameData gameData;
    [SerializeField] private AudioClip hitClip;
    private AudioSource audioSource;

    protected virtual void Start(){
        Invoke("CoolObject", timeToDestroy);
    }

    private void CoolObject(){
        PoolManager.Instance.CoolObject(this.gameObject, type);
    }

    protected virtual void CollisionManager(GameObject collision){
        if (Utilities.IsInLayerMask(collision, enemylayer)){
            EnemyController enemy = collision.GetComponent<EnemyController>();

            if (enemy.GetComponent<EnemyController>() == null){
                Debug.LogError(enemy.name + " doesnt't have EnemyController");
            }else{
                enemy.GetComponent<EnemyController>().TakeDamage(damage + (damage*gameData.extraDamage));
                audioSource = GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>();
                audioSource.PlayOneShot(hitClip);
            }
        }
        if(Utilities.IsInLayerMask(collision.gameObject, layerToDestroy)){
            //if (explosionVFX != null) { Instantiate(explosionVFX, transform.position, transform.rotation); }
            //Destroy(this.gameObject);

            GameObject vfx = PoolManager.Instance.GetPoolObject(PoolObjectType.BulletEffect);
            vfx.transform.position = transform.position;
            vfx.transform.rotation = transform.rotation;
            vfx.gameObject.SetActive(true);

            PoolManager.Instance.CoolObject(this.gameObject, type);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            CollisionManager(collision.gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            CollisionManager(collision.gameObject);
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}
