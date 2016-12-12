using UnityEngine;
using System.Collections;

public class SphereDisplacment : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 v_pos = gameObject.transform.position;
		//v_pos.x += 0.1f;
		gameObject.transform.position = v_pos;
	}
}
