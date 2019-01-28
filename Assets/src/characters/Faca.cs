using UnityEngine;
using System.Collections;

public class Faca : MonoBehaviour {

    public Rigidbody2D rb;
    Vector2 inicialpos;
    private bool respawn;
    public static float delay = 0.3f;
    public float timer;
    void Start() {
        inicialpos = transform.position;
        respawn = false;
        
    }

    // FixedUpdate is called once per frame
    void FixedUpdate() {
        if(respawn && Time.time >= timer) {
            
            transform.position = inicialpos;
            respawn = false;
        }

    }

    public void Fall() {
        rb.gravityScale = 1;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        transform.Translate(new Vector2(100,100));
        respawn = true;
        timer = Time.time + delay;
        rb.gravityScale = 0;
    }
}
