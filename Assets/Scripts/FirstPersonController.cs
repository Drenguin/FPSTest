using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {
	
	public GameObject enemy;
	
	public GUIText gameOverText;
	
	public float movementSpeed = 10.0f;
	public float mouseSensitivity = 2.0f;
	
	float verticalRotation = 0;
	public float verticalRange = 60.0f;
	
	public float jumpSpeed = 10.0f;
	
	float verticalVelocity = 0.0f;
	
	public bool canJump = true;
	
	private float enemySpawnTimer = 0.0f;
	private float enemySpawnTimeLimit = 3.0f;
	
	private bool gameOver;
	
	private int score;
	public GUIText scoreText;

	
	public void UpdateScore() {
		score = score+1;
		scoreText.text = "Score: "+score;
	}

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		enemySpawnTimer = 0.0f;
		gameOver = false;
		gameOverText.text = "";
		score = 0;
		scoreText.text = "Score: 0";
	}
	
	void SpawnEnemy() {
		Vector3 v = new Vector3(10.54f, 1.5f, -17.17f);
		int ranNum = Random.Range(0,3);
		switch(ranNum) {
			case 1:
				v = new Vector3(-20.6f, 1.5f, -13.3f);
				break;
			case 2:
				v = new Vector3(18.7f, 1.5f, 15.6f);
				break;
			case 3:
				v = new Vector3(-20.5f, 1.5f, 10.3f);
				break;
		}
		GameObject.Instantiate(enemy, v, new Quaternion(0,0,0,0));
	}
	
	// Update is called once per frame
	void Update () {
		
		
		//Don't update if game is over
		if(gameOver) {
			return;
		}
		if(Screen.lockCursor == false)
		{
			if(Input.GetMouseButtonDown(0))
			{
				Screen.lockCursor = true;
			}
		} else {
			//Rotation
		
			float rotHorizontal = Input.GetAxis("Mouse X") * mouseSensitivity;
			transform.Rotate(0, rotHorizontal, 0);
			
			verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
			verticalRotation = Mathf.Clamp(verticalRotation, -verticalRange, verticalRange);		
			Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation,0,0);
		}
		
		
		CharacterController cc = GetComponent<CharacterController>();
		
		enemySpawnTimer += Time.deltaTime;
		if(enemySpawnTimer>enemySpawnTimeLimit) {
			enemySpawnTimer = 0.0f;
			if (enemySpawnTimeLimit>0.8f) {
				enemySpawnTimeLimit-=0.05f;
			}
			SpawnEnemy();
		}
		
		
		
		//Movement
		if(cc.isGrounded) {
			verticalVelocity = 0.0f;
		}
		
		float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
		float rightSpeed = Input.GetAxis("Horizontal") * movementSpeed;
		
		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		
		if(canJump && Input.GetButton("Jump") && cc.isGrounded) {
			verticalVelocity = jumpSpeed;
		}
		
		Vector3 speed = new Vector3(rightSpeed,verticalVelocity,forwardSpeed);
		
		speed = transform.rotation * speed;
		
		
		cc.Move(speed * Time.deltaTime);
	}
	
	void OnControllerColliderHit(ControllerColliderHit c) {
		if(c.gameObject.tag == "Enemy") {
			gameOver = true;
			gameOverText.text = "GAME OVER!";
		} else if(c.gameObject.tag == "Ammo") {
			GameObject.Destroy(c.gameObject);
		}
	}
	
	void GameOver() {
		gameOver = true;
		gameOverText.text = "GAME OVER!\nScore: "+score;
		Screen.lockCursor = false;
	}
	
	void OnGUI () {
		if(gameOver) {
			if(GUI.Button(new Rect(Screen.width/2.0f-60,Screen.height*3.0f/4.0f,120,50), "Restart")) {
				Application.LoadLevel(Application.loadedLevelName);
			}
		}
	}
	
	/*void OnCollisionEnter(Collision c) {
		if(c.gameObject.tag == "Enemy") {
			gameOver = true;
			gameOverText.text = "GAME OVER!";
		}
	}*/
}