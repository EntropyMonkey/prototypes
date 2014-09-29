using UnityEngine;
using System.Collections;

public class ShittyLighterFlicker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Light l = GetComponent<Light>();
		l.range += Random.Range(-1f, 1f);
		l.color = new Color(l.color.r + Random.Range(-5f, 5f), 
		                    l.color.g, l.color.b);
	}
}
