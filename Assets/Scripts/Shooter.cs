using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	
	public GameObject bullet;
	public float bulletSpeed = 0.0f;
	private bool gameOver;
	
	void Start() {
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && !gameOver) {
			GameObject shot = GameObject.Instantiate(bullet, new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z) + (Camera.main.transform.forward * 1), Camera.main.transform.rotation) as GameObject;
			
			shot.rigidbody.AddForce(Camera.main.transform.forward * bulletSpeed);
		}
	}
	
	void GameOver() {
		gameOver = true;
	}
}
