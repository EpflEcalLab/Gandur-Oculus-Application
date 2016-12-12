using UnityEngine;
using System.Collections;

public class MoveInstantUser : MonoBehaviour {
	
	[Header("Behavior")]
	[Space(10)]
	public bool setOnTrigger;

	void OnTriggerExit(Collider other){
		//TODO depreciated due to spline
		//DEPRACIATE DUE TO SPLNE
		/*if (other.tag == "Player" && setOnTrigger) {
			other.gameObject.transform.position = new Vector3(ConfigFileReader.Instance.xUserInitialPos,
				ConfigFileReader.Instance.yUserInitialPos,
				ConfigFileReader.Instance.zUserInitialPos);;
			Reset ();
		}*/

	}

	public void Reset(){

		foreach (TriggerToCollider current in this.transform.parent.GetComponentsInChildren<TriggerToCollider>()) {
			current.Reset ();
		}
	}
}
