using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] GameData gameData;
    private float movementSpeed = 10f;
    [SerializeField] private float dashSpeed;
    private float counter;
    private bool dashing;
    private bool isDashing;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private Camera cam;

    private Vector2 movement;
    private Vector2 mousePos;

    void Awake(){
        movementSpeed = gameData.moveSpeed;
    }

    void Update(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        counter -= Time.deltaTime;
        if(counter>0){
            sr.color = Color.grey;
        }else{
            sr.color = Color.white;
        }
        if(gameData.canDash && counter <= 0 && Input.GetKeyDown(KeyCode.Space)){
            dashing = true;
            counter = gameData.timeBtwDash;
        }
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        if(dashing){
            Dash();
            dashing = false;
        }
    }

    private void Dash(){
        trail.enabled = true;
        Physics2D.IgnoreLayerCollision(6, 8, true); //Ignore collision
        isDashing = true;
        Vector2 pos = movement;
        rb.MovePosition(rb.position + pos * dashSpeed * movementSpeed * Time.deltaTime);
        /*Vector2 dashVector = mousePos - (Vector2)transform.position;
        dashVector.Normalize();
        rb.MovePosition(rb.position + dashVector * dashSpeed * 15 * Time.deltaTime);*/
        StartCoroutine(activeCollision(.3f));
    }

    IEnumerator activeCollision(float time){
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreLayerCollision(6, 8, false); //Not ignore collision
        trail.enabled = false;
        isDashing = false;
    }

    public bool GetIsDashing(){
        return isDashing;
    }
}
