using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour {

    public ParticleSystem ps;
    public SpriteRenderer sr;
    public Sprite portal;
    
    // Start is called before the first frame update
    void Start()
    {
        ps.Stop();
        PlayerProgress pp = PlayerProgress.Load();
        if (pp.unlockedLevels[Levels.getIntFromSceneName(SceneManager.GetActiveScene().name) + 1]) {
            sr.sprite = portal;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
