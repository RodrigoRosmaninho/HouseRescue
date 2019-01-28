using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private float velocity;
    public float maxVelocity;
	public float iniVelocity;


    public Rigidbody2D rigidbody;
    public float baseJumpForce = 7.5f;
    public bool isGrounded;

    public bool canMove = true;
    private MovingPlatform movingPlatformScript;

    public CameraMovement camera;

    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public Transform cameraTransform;

    public AudioSource jumpAudio;

    private float lastY;

    // Start is called before the first frame update
    void Start() {
   
        isGrounded = true;
        velocity = iniVelocity;

        cameraTransform = Camera.main.transform;



        lastY = transform.position.y + 1;

    }

   


    // Update is called once per frame
    void FixedUpdate() {

        if (canMove) {
            
            if (Math.Abs(velocity) < maxVelocity) {
                velocity = velocity + maxVelocity * 1f * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Horizontal") == 0) {
                velocity = iniVelocity;
                animator.SetBool("running", false);
            }

            if (Input.GetAxisRaw("Horizontal") == -1) {
                spriteRenderer.flipX = true;
                animator.SetBool("running", true);
            } else if (Input.GetAxisRaw("Horizontal") == 1) {
                spriteRenderer.flipX = false;
                animator.SetBool("running", true);
            }

            float offset = 0;
            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            if (movingPlatformScript != null && movingPlatformScript.sentidoInicial == horizontalMovement * -1)
                offset = movingPlatformScript.defaultVelocity;
            transform.Translate(new Vector2((velocity + offset) * Time.deltaTime * horizontalMovement, 0f));
        }

    }

    void Update(){
        if (canMove && Input.GetButtonDown("Jump")) Jump();
    }

    // Die when touched by enemies or by environmental obstacles
    public void Die(){
        canMove = false;
        transform.GetComponent<BoxCollider2D>().enabled = false;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, baseJumpForce * 0.8f);
        Invoke("resetLevel", 0.4f);
    }

    public void resetLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Jump() {
        if (!isGrounded) {
            
            return;
        }
        rigidbody.velocity = new Vector2(0, baseJumpForce + velocity/3f);
        isGrounded = false;
        jumpAudio.Play();
    }

    void OnCollisionEnter2D(Collision2D col) {
        
        if (col.gameObject.tag == "enemy") {
            Die();
        }
        if(col.gameObject.tag == "wall") {
            isGrounded = true;
 
        }
        if (col.gameObject.tag == "objective") {
            Debug.Log("Objective!");
            ParticleSystem ps = col.gameObject.GetComponent<Objective>().ps;
            ps.Play();
            Invoke("completeLevel", 1f);
        }
    }




    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "enemy") {
            Die();
        }
        if (col.gameObject.tag == "ground") {
            isGrounded = true;
        }
        if (col.gameObject.tag == "cameraTrigger") {
            int index = Int32.Parse(col.gameObject.name.Substring(0, 1));
            
            cameraTransform.position = new Vector3(camera.cameraPos[index].x, camera.cameraPos[index].y, cameraTransform.position.z);
            
        }
        

    }
    
    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.tag == "platform"){
            transform.parent = other.transform;
            movingPlatformScript = transform.parent.GetComponent<MovingPlatform>();
            isGrounded = true;
        }
        if (other.gameObject.tag == "ground") {
            isGrounded = true;
        }

    }
 
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "platform"){
            transform.parent = null;
            movingPlatformScript = null;
        }
        

    }  
    
    void completeLevel(){
        PlayerProgress pp = PlayerProgress.Load();
        pp.unlockLevel(Levels.getIntFromSceneName(SceneManager.GetActiveScene().name));
        pp.Save();
        Debug.Log("Level completed! Progress Saved!");
        SceneManager.LoadScene("House");
    }
}    
