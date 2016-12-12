using UnityEngine;
using System.Collections;

public class TransitionByAnim : MonoBehaviour {
	public Animator anim;
	public bool transition;
	public bool reverse = false;

	private bool once = true;

	// Use this for initialization
	void Start () {
	
	}

	void OnEnable() {
		Reset ();

	}

	void Update () {
		if (transition && once) {
			transition = false;
			once = false;
			anim.SetTrigger("Transition");
		}
	}


	void OnTriggerEnter(Collider other){
		if (other.tag != "Player" && anim.isInitialized)
			return;
		if (reverse) {
			anim.SetTrigger ("TransitionReverse");
			anim.ResetTrigger ("Transition");
		} else {
			anim.ResetTrigger ("TransitionReverse");
			anim.SetTrigger ("Transition");
		}
	}


	public void Reset(){

		anim.SetTrigger("Reset");
		anim.ResetTrigger ("Transition");
		anim.ResetTrigger ("TransitionReverse");
		once = true;
		transition = false;
	}


}
