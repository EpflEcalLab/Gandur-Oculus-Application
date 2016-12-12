using UnityEngine;
using System.Collections;

public class CommandsUser : MonoBehaviour {

	public GameObject user;

	void Awake () {

		//Add game object relative to user displacement
		gameObject.AddComponent<SceneGameObjects>();
		UserDisplacementType v_displacementType = gameObject.AddComponent<UserDisplacementType>();
		gameObject.GetComponent<UserSplineAnimator> ().spline = v_displacementType.splineUserDisplacement;
	}


}
