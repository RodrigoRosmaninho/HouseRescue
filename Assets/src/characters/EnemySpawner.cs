using UnityEngine;

public class EnemySpawner : MonoBehaviour{
    public GameObject spawnee;
    private float lastTime;
    public float activeDuration, hiddenDuration;

    void Start(){
        lastTime = Time.fixedTime;
        spawnee.SetActive(false);
    }

    void FixedUpdate(){
        float limit = spawnee.activeSelf ? activeDuration : hiddenDuration;
        if ((Time.fixedTime - lastTime) >= limit) {
            spawnee.SetActive(!spawnee.activeSelf);
            lastTime = Time.fixedTime;
        }
    }
}
