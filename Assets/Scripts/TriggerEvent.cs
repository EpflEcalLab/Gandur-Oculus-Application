using UnityEngine;
using System.Collections;

public class TriggerEvent : MonoBehaviour {
	public LightUp scriptLightUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag != "Player")
			return;
		scriptLightUp.lightChange = true;
	}

}
