using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	//public GameObject playerObject;
	public float moveSpeed = 4.0f;
	public float scale = 1.0f;
	
	// Use this for initialization
	void Start () {
		renderer.material.color = new Color(1,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		/*if(scale<4.0f) {
			scale+=0.5f * Time.deltaTime;
			moveSpeed+=0.1f * Time.deltaTime;
		}
		transform.localScale = new Vector3(scale, scale, scale);*/
		var playerObject = GameObject.Find("Player");
		transform.LookAt(playerObject.transform);
		transform.rotation = new Quaternion(0, transform.rotation.y, 0,transform.rotation.w);
		//transform.position += transform.forward*moveSpeed*Time.deltaTime;
		rigidbody.AddForce(transform.forward*moveSpeed*Time.deltaTime);
	}
	
	void OnCollisionEnter(Collision c) {
		if(c.gameObject.tag == "Player") {
			GameObject.Destroy(gameObject);
			var playerObject = GameObject.Find("Player");
			playerObject.SendMessage("GameOver");
		}
		
	}
}
