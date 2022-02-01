
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarEnemy : MonoBehaviour{
    [SerializeField] ParticleSystem ps;
    List<ParticleCollisionEvent> eventCOl = new List<ParticleCollisionEvent>();

    void OnParticleCollision(GameObject col){
        if(!col.GetComponent<MovementController>().GetIsDashing()){
            int events = ps.GetCollisionEvents(col, eventCOl);
            for(int i = 0; i < events; i++){
                HealthController health = col.GetComponent<HealthController>();
                if(health != null){
                    health.TakeDamage();
                }
        }
        }
    }
}
