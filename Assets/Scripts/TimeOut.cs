using UnityEngine;
using System.Collections;

public class TimeOut : MonoBehaviour {

	private float afkTime;
	private float timeOutTimeSec = 20f;//seconde
	private Controler script;

	void Start () {
		script =  FindObjectOfType(typeof(Controler)) as Controler;
	}

	void Update () {
	
		afkTime += Time.deltaTime;
		if (afkTime > timeOutTimeSec) {
			script.AppReset ();
			ResetAfkTimeOut ();
		}

		if (Input.GetKey (Ressources.keyForward)) {
			ResetAfkTimeOut ();
		}
		if (Input.GetKey (Ressources.keyBackward)) {
			ResetAfkTimeOut ();
		}

	}
		

	public void ResetAfkTimeOut(){
		afkTime = 0;
	}
}
