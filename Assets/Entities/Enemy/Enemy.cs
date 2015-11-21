using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float health;
    public float projectileSpeed;
    public float shotsPerSecond;
    public GameObject projectile;

    void OnTriggerEnter2D(Collider2D collision) {       
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile && missile.tag != "EnemyLaser") {
            missile.Hit();
            health -= missile.GetDamage();
            if (health <= 0) {                
                Destroy(this.gameObject);
            }            
        }
    }

    private void Update() {
        float probability = Time.deltaTime * shotsPerSecond;
        if(Random.value < probability) {
            Fire();
        }        
    }

    private void Fire() {
        GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        beam.rigidbody2D.velocity = new Vector3(0, -projectileSpeed);
        
    }
}
