using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject X;
    [SerializeField] private GameObject player;

    private bool shopOpen = false;

    void Start(){
        shopUI.SetActive(false);
        X.SetActive(false);
        shopOpen = false;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            X.SetActive(true);
            if(Input.GetKeyDown(KeyCode.X)||Input.GetKeyDown(KeyCode.Escape)){
                OpenCloseShop();
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider){
        if(collider.CompareTag("Player") && (Input.GetKeyDown(KeyCode.X)||Input.GetKeyDown(KeyCode.Escape))){
            OpenCloseShop();
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            X.SetActive(false);
        }
    }
    public void OpenCloseShop(){
        shopOpen = !shopOpen;
        X.SetActive(!shopOpen);
        shopUI.SetActive(shopOpen);
        player.GetComponent<MovementController>().enabled = !shopOpen;
        player.GetComponent<WeaponController>().enabled = !shopOpen;
    }
}
