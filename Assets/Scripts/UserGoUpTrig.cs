using UnityEngine;
using System.Collections;

public class UserGoUpTrig : MonoBehaviour {
	public CommandsUser script;
	public bool could;
	// Use this for initialization
	void Start () {

		//TODO depreciated due to spline
		//script.userCanGoUp = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other){
	//TODO depreciated due to spline
		/*	if (other.tag != "Player")
			return;
		script.userCanGoUp = could;*/

	}
}
