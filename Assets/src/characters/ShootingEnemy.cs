using UnityEngine;

public class ShootingEnemy : Enemy {
    public Transform firePoint;
    public GameObject bulletPreFab;
    private float lastTime;
    public float shotFrequency;
    public float bulletVelocity = 10;
    
    public override void Start(){
        base.Start();
        lastTime = Time.fixedTime;
    }

    // Update is called once per frame
    public override void FixedUpdate() {
        base.FixedUpdate();
        if ((Time.fixedTime - lastTime) >= shotFrequency) {
            Shoot();
            lastTime = Time.fixedTime;
        }
    }

    void Shoot(){
        GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().speed = bulletVelocity;

    }


}