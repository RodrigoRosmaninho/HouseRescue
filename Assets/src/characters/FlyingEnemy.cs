using UnityEngine;
using System.Collections;

public class FlyingEnemy : PatrollingEnemy {

    public float delay;
    private float timer;
    public bool startUp;
    private bool isUp;
    public float upY, downY;
    private int direction;
    
    public override void Start() {
        base.Start();
        if (startUp) transform.position = new Vector2(transform.position.x, upY);
        else transform.position = new Vector2(transform.position.x, upY);
        timer = Time.time + delay;
        isUp = startUp;
        direction = 0;
    }

    // Update is called once per frame
    public override void FixedUpdate() {
        base.FixedUpdate();

        if (transform.position.y > upY || transform.position.y < downY) {
            direction = 0;
        }

        if (timer <= Time.time) {
            if (isUp) {
                direction = -1;
            } else {
                direction = 1;
            }
            isUp = !isUp;
            timer = Time.time + delay;
        }

        

        transform.Translate(new Vector2(0, 5 * Time.deltaTime * direction));
    }
}
