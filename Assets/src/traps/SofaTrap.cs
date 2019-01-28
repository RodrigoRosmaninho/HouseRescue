using UnityEngine;

public class SofaTrap : MonoBehaviour {
    private float startTime;
    public float waitForSeconds;
    private bool isClosed, trapActivated, killPlayer;
    public Animator anim;
    private Player player;
    
    // Start is called before the first frame update
    void Start(){
        if (waitForSeconds == 0) waitForSeconds = 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        isClosed = anim.GetCurrentAnimatorStateInfo(0).IsName("Sofa");
        if (isClosed) {
            if(killPlayer && player != null) player.Die();
            anim.SetBool("trapActivate", false);
        }
        else if (trapActivated && (Time.fixedTime - startTime) >= waitForSeconds) {
            trapActivated = false;
            anim.SetBool("trapActivate", true);
            if (killPlayer && player != null) player.canMove = false;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other){
        if (!isClosed && other.transform.tag == "player") {
            startTime = Time.fixedTime;
            trapActivated = true;
            killPlayer = !isClosed;
            player = other.transform.GetComponent<Player>();
        }
    }
    
    private void OnCollisionExit2D(Collision2D other){
        if (other.transform.tag == "player") killPlayer = false;
    }
}
