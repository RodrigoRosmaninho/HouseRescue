using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{

    public Vector2[] cameraPos = new Vector2[0];

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void returnToHouse(){
       SceneManager.LoadScene("House");
    }

    public void returnToMenu(){
       SceneManager.LoadScene("Menu");
    }
}
