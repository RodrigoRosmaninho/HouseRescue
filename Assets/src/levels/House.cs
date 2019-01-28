using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class House : MonoBehaviour
{
    public float BedRoomPortalMinX, BedRoomPortalMaxX;

    private Vector3 inicialPos;
    private int direction = 1;
    private float time;

    public GameObject player;

    public Player playerScript;
	
	public SpriteRenderer[] family = new SpriteRenderer[0];
    public SpriteRenderer[] arrows = new SpriteRenderer[0];

    public PlayerProgress playerProgress;    

    private float shakingFactor;

    private float Timer;

    private int currentRoom;

    private float cameraTarget;
    void Start(){
        time = Time.fixedTime;
        inicialPos = transform.position;
        
        shakingFactor = 0.005f;

        playerProgress = PlayerProgress.Load();

        currentRoom = 1;

        cameraTarget = 0;

        playerProgress.LogProgress();
		
		for(int i = 0; i < family.Length; i++){
			if(playerProgress.unlockedLevels[i+2]){
				family[i].enabled = true;
                arrows[i].enabled = true;
            }else{
				family[i].enabled = false;
                arrows[i].enabled = false;
            }
		}

        if (playerProgress.unlockedLevels[5]) winGame(); // if last level finished, end game

        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update(){
        ArrowAnimation();
        
        CameraShake();
        if (shakingFactor >= 0.15f) { 
  
            if (transform.position.x < 10) {
                SceneManager.LoadScene("Level1");
            }else if (transform.position.x < 30) {
                SceneManager.LoadScene("Level2");
            } else if (transform.position.x < 50) {
                SceneManager.LoadScene("Level3");
            } else if (transform.position.x < 70) {
                SceneManager.LoadScene("Level4");
            }

        }

        if (transform.position.x + 6 <= player.transform.position.x && playerProgress.unlockedLevels[currentRoom+1] && currentRoom != 5) {
            if(currentRoom == 4) SceneManager.LoadScene("End");
            LoadNextRoom();
        }
        if (transform.position.x - 6 >= player.transform.position.x && playerProgress.unlockedLevels[currentRoom-1]) {
            LoadPrevRoom();
        }

        if(transform.position.x < cameraTarget) {
            transform.Translate(new Vector2(20*Time.deltaTime,0));
            
        }
        if (transform.position.x > cameraTarget) {
            transform.Translate(new Vector2(-20 * Time.deltaTime, 0));
            
        }
        if (Math.Abs(transform.position.x - cameraTarget) < 1)
            playerScript.canMove = true;
        

    }

    private void ArrowAnimation(){
        foreach (var arrow in arrows) {
            arrow.transform.Translate(new Vector2((Time.deltaTime * direction) / 2, 0f));
        }

        if ((Time.fixedTime - time) >= 0.3) {
            time = Time.fixedTime;
            direction *= -1;
        }
    }

    private void CameraShake() {
        if (player.transform.localPosition.x >= BedRoomPortalMinX && player.transform.localPosition.x <= BedRoomPortalMaxX && Math.Abs(transform.position.x - cameraTarget) < 5 ){
            transform.localPosition = inicialPos + UnityEngine.Random.insideUnitSphere * shakingFactor;
            shakingFactor += 0.1f * Time.deltaTime;
            playerScript.canMove = true;
        } 
        else {
            shakingFactor = 0.005f;
        }
    }
    private void LoadNextRoom() {
        Debug.Log("Next");
        cameraTarget += 20;
        inicialPos = new Vector3(cameraTarget, inicialPos.y, inicialPos.z);
        player.transform.localPosition = new Vector3(-4,-1,10);
        currentRoom++;
        playerScript.canMove = false;
    }
    private void LoadPrevRoom() {
        cameraTarget -= 20;
        inicialPos = new Vector3(cameraTarget, inicialPos.y, inicialPos.z);
        player.transform.localPosition = new Vector3(-4, -1, 10);
        currentRoom--;
        playerScript.canMove = false;
    }

    private void winGame(){
        // display something at the end of the game
    }
}
