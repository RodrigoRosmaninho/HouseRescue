using UnityEngine;

public class BouncingEnemy : Enemy {

    public Rigidbody2D rigidbody;
    private bool isGrounded;
    public float jumpForce;

    public float maxX, minX;
    public int sentidoInicial;

    public bool rotate;

    public Transform rotationObject;


    public static float defaultVelocity = 1.5f;
    public override void Start() {
        base.Start();
        isGrounded = false;
        if(velocity == 0)velocity = defaultVelocity;
    }

    // Update is called once per frame
    public override void FixedUpdate() {
        base.FixedUpdate();


        transform.Translate(new Vector2(velocity * sentidoInicial * Time.deltaTime, 0));
        
        if (transform.position.x >= maxX) {
            sentidoInicial = -1;
        }
        if (transform.position.x <= minX) {
            sentidoInicial = 1;
        }

        if (isGrounded) Jump();

        if (rotate) {
            rotationObject.Rotate(0,0,-90*Time.deltaTime * sentidoInicial, Space.Self);
        }
        
    }

    public void Jump() {
        if (!isGrounded) return;
        rigidbody.velocity = new Vector2(0, jumpForce);
        isGrounded = false;
    }


    
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "wall") {
            isGrounded = true;
        }
    }

    
}
