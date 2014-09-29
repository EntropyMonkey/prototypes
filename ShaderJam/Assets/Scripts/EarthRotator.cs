using UnityEngine;
using System.Collections;

public class EarthRotator : MonoBehaviour {

	public float speed;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(Vector3.up, speed);
	}
}
