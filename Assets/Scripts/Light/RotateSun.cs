using UnityEngine;
using System.Collections;

public class RotateSun : MonoBehaviour {

	public float m_rotationSpeed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.left * (m_rotationSpeed * Time.deltaTime));
	}
}
