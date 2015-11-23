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
    public AudioClip enemyHitSound;

    private ScoreKeeper scoreKeeper;
    private EnemySpawner spawnerLevel;

    void Start() {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        explosionSound = GameObject.FindObjectOfType<AudioClip>();
        spawnerLevel = GameObject.FindObjectOfType<EnemySpawner>();

        //shots per second increases with level
        shotsPerSecond = spawnerLevel.beginningLevel / 3;
    }

    void OnTriggerEnter2D(Collider2D collision) {       
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile && missile.tag != "EnemyLaser") {
            missile.Hit();
            AudioSource.PlayClipAtPoint(enemyHitSound, transform.position);
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
