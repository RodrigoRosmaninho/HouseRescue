using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeedingPatrollingEnemy : Enemy {
    public float iniVelocity;
    public float maxX, minX, maxVelocity;
    public int sentidoInicial;
    public float speedingFactor;

    public SpriteRenderer spriteRenderer;

    public override void Start() {
        base.Start();
        velocity = iniVelocity;

    }

    public override void FixedUpdate() {

        base.FixedUpdate();

        if (Math.Abs(velocity) < maxVelocity) {
            velocity = velocity + maxVelocity * speedingFactor * Time.deltaTime;
        }


        
        if (transform.position.x >= maxX) {
            sentidoInicial = -1;
            spriteRenderer.flipX = false;
            velocity = iniVelocity;
        }else if (transform.position.x <= minX) {
            sentidoInicial = 1;
            spriteRenderer.flipX = false;
            velocity = iniVelocity;
        }

        transform.Translate(new Vector2(velocity * sentidoInicial * Time.deltaTime, 0));
    }


}
