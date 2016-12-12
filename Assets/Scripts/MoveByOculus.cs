using UnityEngine;
using System.Collections;

public class MoveByOculus : MonoBehaviour {
	
	public Transform focusPts;
	public bool active;
	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			Vector3 temp = this.transform.position;
			if (temp.z > focusPts.position.z+0.1f) {
				temp.z -= speed;
			} else if (temp.z < focusPts.position.z-0.1f) {
				temp.z += speed;
			}
			//temp.z = focusPts.position.z;

			this.transform.position = temp;
		}
	}
}
