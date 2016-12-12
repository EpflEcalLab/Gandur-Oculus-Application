using UnityEngine;
using System.Collections;

public class ActiveTracking : MonoBehaviour {
	public MoveByOculus script;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag != "Player")
			return;
		script.active = true;
	}
}
