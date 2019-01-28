using UnityEngine;
using System.Collections;

public class Facas : MonoBehaviour {
    
    public int[] order = new int[5];
    public Faca[] facas = new Faca[5];

    public float delay;
    public float timer;
    public int currentFaca;
    void Start() {
        timer = Time.time + delay;
        currentFaca = 0;

    }

    // Update is called once per frame
    void FixedUpdate() {
        
        if(Time.time >= timer) {
            Debug.Log("faca");
            facas[order[currentFaca++]].Fall();
            timer = Time.time + delay;
            if (currentFaca == 5) currentFaca = 0;
        }
    }
}
