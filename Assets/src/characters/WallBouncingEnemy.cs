using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBouncingEnemy : Enemy
{

    public Rigidbody2D rigidbody;
    private bool isGrounded;
    public float jumpForce;

    public float maxX, minX;
    public int sentidoInicial;

    public SpriteRenderer spriteRenderer;

    public bool rotate;

    public Transform rotationObject;

    public static float defaultVelocity = 2;
    void Start()
    {
        base.Start();
        isGrounded = false;
        if (velocity == 0) velocity = defaultVelocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdate();


        transform.Translate(new Vector2(velocity * sentidoInicial * Time.deltaTime, 0));

        if (transform.position.x >= maxX) {
            sentidoInicial = -1;
            spriteRenderer.flipX = false;
            Jump();
        }
        if (transform.position.x <= minX) {
            sentidoInicial = 1;
            spriteRenderer.flipX = false;
            Jump();
        }

        if (rotate) {
            rotationObject.Rotate(0, 0, -90 * Time.deltaTime * sentidoInicial, Space.Self);
        }
    }

    public void Jump() {
        if (!isGrounded) return;
        rigidbody.velocity = new Vector2(0, jumpForce);
        isGrounded = false;
    }


    public void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "ground") {
            isGrounded = true;
        }
    }
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "wall") {
            isGrounded = true;
        }
        if (col.gameObject.tag == "enemy") {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(),GetComponent<Collider2D>());
        }

    }

}
