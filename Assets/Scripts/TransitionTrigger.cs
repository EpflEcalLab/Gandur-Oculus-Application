using UnityEngine;
using System.Collections;

public class TransitionTrigger : MonoBehaviour {
	
	public float delay = 0f;
	public GameObject[] OFF;
	public GameObject[] ON;

	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
		
	void OnTriggerEnter(Collider other){
		if (other.tag != "Player")
			return;
		foreach(GameObject current  in OFF){
			if (current != null)
				current.SetActive (false);
			else
				Debug.Log ("Exeception  Null : OFF");
		}
		StartCoroutine(EndOfColliderEnter ());
	}

	IEnumerator EndOfColliderEnter(){
		yield return new WaitForSeconds (delay);
		foreach(GameObject current  in ON){
			if(current!=null)
				current.SetActive (true);
			else 
				Debug.Log("Exeception  Null : ON" );
		}
	}
}
