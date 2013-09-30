using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform)
		{
		  //child is your child transform
			child.renderer.material.color = new Color(0.1f,0.1f,0.1f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,50*Time.deltaTime,0);
	}
}
