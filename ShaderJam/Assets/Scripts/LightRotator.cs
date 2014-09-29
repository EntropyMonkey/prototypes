using UnityEngine;
using System.Collections;

public class LightRotator : MonoBehaviour {

	public float speed;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(Vector3.up, speed);
		transform.Rotate(Vector3.forward, speed);
	}
}
