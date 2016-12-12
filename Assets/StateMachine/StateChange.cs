using UnityEngine;
using System.Collections;

public class StateChange : MonoBehaviour {

	public int nextState;
	public Animator stateMachine;

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player"){
			stateMachine.SetInteger (Ressources.stateMachine_nameState, nextState);
		}
	}

}