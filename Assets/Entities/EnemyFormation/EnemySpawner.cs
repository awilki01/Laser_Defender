﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width;
    public float height;
    public float speed;

    private bool movingRight = true;
    private float xmax;
    private float xmin;

	// Use this for initialization
	void Start () {
        //Calculate viewport edges on left and right
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 LeftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xmax = rightBoundary.x;
        xmin = LeftBoundary.x;
        
        foreach (Transform child in transform) {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;

            // this puts the spawned enemy ship under the Position pre-fab in editor
            enemy.transform.parent = child;           
        }
	}

    public void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
	
	// Update is called once per frame
	void Update () {
	    if (movingRight) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        } else {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        if (leftEdgeOfFormation < xmin) {
            movingRight = true;
        } else if (rightEdgeOfFormation > xmax) {
            movingRight = false;
        }
	}
}