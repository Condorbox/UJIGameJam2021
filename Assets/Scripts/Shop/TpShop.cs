using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TpShop : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Camera cam;
    [SerializeField] private float newOrthographicSize;
    private float orthographicSize;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject shop;
    [SerializeField] private SpawnerController spawn;
    [SerializeField] private Transform shopPoint;
    [SerializeField] private Transform startShopPosition;
    [SerializeField] private GameObject canvas;
    [SerializeField, ReadOnly] private bool tpToShop  = true;

    void OnEnable(){
        canvas.SetActive(false);
    }

    void Awake(){
        orthographicSize = cam.orthographicSize;
        shop.SetActive(false);
        tpToShop = true;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            if(tpToShop){
                StartCoroutine(OpenShop()); 
            }else{
                StartCoroutine(CloseShop()); 
            }
            tpToShop = !tpToShop;
        }
    }

    IEnumerator OpenShop(){
        player.GetComponent<MovementController>().enabled = false;
        player.GetComponent<WeaponController>().enabled = false;
        anim.SetTrigger("Tp");
        yield return new WaitForSeconds(1f);
        player.transform.position = Vector3.zero;
        shop.SetActive(true);
        cam.orthographicSize = newOrthographicSize;
        transform.position = shopPoint.position;
        player.GetComponent<MovementController>().enabled = true;
        player.GetComponent<WeaponController>().enabled = true;
    }

    IEnumerator CloseShop(){
        player.GetComponent<MovementController>().enabled = false;
        player.GetComponent<WeaponController>().enabled = false;
        anim.SetTrigger("Tp");
        yield return new WaitForSeconds(1f);
        shop.SetActive(false);
        cam.orthographicSize = orthographicSize;
        transform.position = startShopPosition.position;
        player.GetComponent<MovementController>().enabled = true;
        player.GetComponent<WeaponController>().enabled = true;
        spawn.RestToSpawn();
    }

}
