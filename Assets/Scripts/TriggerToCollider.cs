using UnityEngine;
using System.Collections;

public class TriggerToCollider : MonoBehaviour {

	private Vector3 posUser;
	// Use this for initialization

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player"){
			posUser = other.gameObject.transform.position;
		}
	}
	void OnTriggerExit(Collider other){

		if (other.tag == "Player" && posUser.x < other.transform.position.x){
			gameObject.GetComponent<BoxCollider> ().isTrigger = false;
		}
	}

	public void Reset(){

		this.gameObject.GetComponent<BoxCollider> ().isTrigger = true;

	}
}
