using System;
using UnityEngine;

public class ChasingEnemy : Enemy{
    private float lastTime, lastTimeSeen;

    public float rayCastFrequency, timeUntilGiveUp, detectionDistance;

    public static float defaultVelocity = 4;
    private int direction = 0;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public override void Start(){
        base.Start();
        lastTime = Time.fixedTime;
        if(velocity == 0)velocity = defaultVelocity;
    }

    // Update is called once per frame
    public override void FixedUpdate() {
        base.FixedUpdate();
        if (direction == 0) animator.SetBool("running", false);
        else animator.SetBool("running", true);

        if(direction == -1) {
            spriteRenderer.flipX = false;
        }else if (direction == 1) {
            spriteRenderer.flipX = true;
        }

        transform.Translate(new Vector2(direction * velocity * Time.deltaTime,0));
        if ((Time.fixedTime - lastTime) >= rayCastFrequency) {
            Verify();
            lastTime = Time.fixedTime;
        }
        
    }

    void Verify(){
        RaycastHit2D hitInfoRight = Physics2D.Raycast(transform.position, transform.right);
        RaycastHit2D hitInfoLeft = Physics2D.Raycast(transform.position, Quaternion.Euler(0, -180, 0) * transform.right);
        
        if (hitInfoRight.transform.tag == "player" && Math.Abs(hitInfoRight.transform.position.x - transform.position.x) <= detectionDistance) {
            lastTimeSeen = Time.fixedTime;
            direction = 1;
        }

        if (hitInfoLeft.transform.tag == "player" && Math.Abs(hitInfoLeft.transform.position.x - transform.position.x) <= detectionDistance) {
            lastTimeSeen = Time.fixedTime;
            direction = -1;
        }

        if ((Time.fixedTime - lastTimeSeen) >= timeUntilGiveUp) direction = 0;
    }

}