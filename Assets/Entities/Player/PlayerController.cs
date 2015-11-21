using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float padding;
    public float projectileSpeed;
    public float firingRate;
    public float health;
    public GameObject projectile;

    private float xmin;
    private float xmax;
    private float newX;

	// Use this for initialization
	void Start () {

        //Calculate edge of camera view to set ship boundaries
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding; 
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A)){
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D)) {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            InvokeRepeating("Fire", 0.0000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            CancelInvoke("Fire");
        }

        //Clamp ship to gamespace
        newX = Mathf.Clamp(this.transform.position.x, xmin, xmax);
        this.transform.position = new Vector3(newX, this.transform.position.y, this.transform.position.z);
    }

    private void MoveLeft() {
        this.transform.position += Vector3.left * speed * Time.deltaTime;        
    }

    private void MoveRight() {
        this.transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void Fire() {
        GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        beam.rigidbody2D.velocity = new Vector3(0, projectileSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile && missile.tag == "EnemyLaser") {
            Debug.Log("SHIP HIT!!!    " + health);
            missile.Hit();
            health -= missile.GetDamage();
            if (health <= 0) {
                Destroy(this.gameObject);
            }
        }
    }

}
