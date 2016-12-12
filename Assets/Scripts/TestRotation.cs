using UnityEngine;
using System.Collections;
using DG.Tweening;
public class TestRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {

	
		Tween v_mytWEEN = this.gameObject.transform.DORotate(new Vector3 (160, 0, 0), 5f);
		v_mytWEEN.Play ();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
