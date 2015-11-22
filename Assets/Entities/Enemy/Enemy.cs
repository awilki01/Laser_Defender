using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float health;
    public float projectileSpeed;
    public float shotsPerSecond;    
    public GameObject projectile;
    public int hitValue = 150;
    public int destroyValue = 300;
    public AudioClip explosionSound;
    public AudioClip fireSound;
    public AudioClip deathSound;

    private ScoreKeeper scoreKeeper;

    void Start() {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        explosionSound = GameObject.FindObjectOfType<AudioClip>();
    }

    void OnTriggerEnter2D(Collider2D collision) {       
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile && missile.tag != "EnemyLaser") {
            missile.Hit();
            scoreKeeper.Score(hitValue);            
            health -= missile.GetDamage();                                  
            if (health <= 0) {
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
                scoreKeeper.Score(destroyValue);                
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
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
        
    }
}
