using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float padding;

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

}
