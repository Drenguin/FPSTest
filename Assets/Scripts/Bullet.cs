using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	
	
	void Start() {
		renderer.material.color = new Color(0,0,0);
		rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
	}

	void OnCollisionEnter(Collision c) {
		GameObject.Destroy(gameObject);
		if (c.gameObject.tag == "Enemy") {
			var playerObject = GameObject.Find("Player");
			playerObject.SendMessage("UpdateScore");
			GameObject.Destroy(c.gameObject);
		}
	}
}
