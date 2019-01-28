using UnityEngine;
public class PatrollingEnemy : Enemy {
    public float maxX, minX;
    public int sentidoInicial;
    public override void Start() {
        base.Start();
    }

    public override void FixedUpdate() {

        base.FixedUpdate();
        
        transform.Translate(new Vector2(velocity * sentidoInicial * Time.deltaTime, 0));
        if (transform.position.x >= maxX) {
            sentidoInicial = -1;
        }
        if (transform.position.x <= minX) {
            sentidoInicial = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "enemy") {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(),GetComponent<Collider2D>());
        }
    }
}
