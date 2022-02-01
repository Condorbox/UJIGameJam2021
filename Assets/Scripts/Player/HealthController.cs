using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject dieCanvas;
    [SerializeField] private GameObject armor;
    [SerializeField] private SpawnerController spawnerController;
    [SerializeField] private AudioClip defeatClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject deathEffect;

    void Awake(){
        dieCanvas.SetActive(false);

        if(gameData.armor){
            armor.SetActive(true);
        }else{
            armor.SetActive(false);
        }
    }

    void Update(){
        if(gameData.armor){
            armor.SetActive(true);
        }else{
            armor.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ManageCollisions(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ManageCollisions(collision.gameObject);
    }

    private void ManageCollisions(GameObject col){
        if(col.CompareTag("Enemy")){
            Die();
        }
    }

    public void TakeDamage(){
        Die();
    }



    private void Die(){
        if(gameData.armor){
            gameData.armor = false;
        }else{
            Instantiate(deathEffect, transform.position, transform.rotation);
            dieCanvas.SetActive(true);
            spawnerController.enabled = false;
            audioSource.clip = defeatClip;
            audioSource.loop = false;
            audioSource.PlayOneShot(defeatClip);
            this.gameObject.SetActive(false);
        }
    }
}
