using UnityEngine;

public class MovingPlatform : MonoBehaviour{
    public float defaultVelocity = 3;
    public float maxX, minX;
    public int sentidoInicial;
    public void Start() {
     
    }

    public void FixedUpdate() {
        
        transform.Translate(new Vector2(defaultVelocity * sentidoInicial * Time.deltaTime, 0));
        if (transform.position.x >= maxX) {
            sentidoInicial = -1;
        }
        if (transform.position.x <= minX) {
            sentidoInicial = 1;
        }
    }
}