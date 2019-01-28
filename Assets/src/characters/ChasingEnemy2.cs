using System;
using UnityEngine;

public class ChasingEnemy2 : Enemy{
    public float maxX, minX;
    private float lastTime;
    public float rayCastFrequency, detectionDistance;
    public static float defaultVelocity = 2;
    private int direction = 0;
    private bool chaseAllowed = false;
    private Transform player;
    
    public override void Start(){
        base.Start();
        lastTime = Time.fixedTime;
        velocity = defaultVelocity;
    }

    // Update is called once per frame
    public override void FixedUpdate() {
        base.FixedUpdate();

        if (!chaseAllowed) {
            direction = 0;
            if ((Time.fixedTime - lastTime) >= rayCastFrequency) {
                Verify();
                lastTime = Time.fixedTime;
            }
        }
        else {
            float diff = player.position.x - transform.position.x;
            if (Math.Abs(diff) < 0.1) direction = 0;
            else direction = (int) (diff / Math.Abs(diff));
            Debug.Log(direction);
        }
        
        if (transform.position.x >= maxX && direction == 1) {
            direction = 0;
        }
        if (transform.position.x <= minX && direction == -1) {
            direction = 0;
        }
        transform.Translate(new Vector2(direction * velocity * Time.deltaTime,0));
        
    }

    void Verify(){
        RaycastHit2D hitInfoRight = Physics2D.Raycast(transform.position, transform.right);
        RaycastHit2D hitInfoLeft = Physics2D.Raycast(transform.position, Quaternion.Euler(0, -180, 0) * transform.right);

        if (hitInfoRight.transform.tag == "player" && Math.Abs(hitInfoRight.transform.position.x - transform.position.x) <= detectionDistance) {
            player = hitInfoRight.transform;
            chaseAllowed = true;
        }

        if (hitInfoLeft.transform.tag == "player" && Math.Abs(hitInfoLeft.transform.position.x - transform.position.x) <= detectionDistance) {
            player = hitInfoLeft.transform;
            chaseAllowed = true;
        }
    }

}