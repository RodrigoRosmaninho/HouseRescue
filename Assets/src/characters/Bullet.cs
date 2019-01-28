using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed;

    public Rigidbody2D rb;
    private void Start(){
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag != "enemy") Destroy(gameObject);
    }
}
