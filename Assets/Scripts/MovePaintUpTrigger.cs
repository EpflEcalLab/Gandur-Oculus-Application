using UnityEngine;
using System.Collections;

public class MovePaintUpTrigger : MonoBehaviour {

	public Animator animPainting;



	void OnTriggerEnter(Collider other){
		if(other.tag =="Player")
			animPainting.SetTrigger ("MoveUp");

	}
}
