using UnityEngine;
using System.Collections;

public class ResetOnStandBy : MonoBehaviour {

	public float angleMagnitude = 0.5f;

	private TimeOut script;
	private Vector3 savedAngles;
	// Use this for initialization
	void Start () {
		script =  FindObjectOfType(typeof(TimeOut)) as TimeOut;
		savedAngles = this.gameObject.transform.eulerAngles;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if ((this.gameObject.transform.eulerAngles - savedAngles).magnitude >= angleMagnitude) {
			script.ResetAfkTimeOut ();
		}
		savedAngles = this.gameObject.transform.eulerAngles;
	}
}