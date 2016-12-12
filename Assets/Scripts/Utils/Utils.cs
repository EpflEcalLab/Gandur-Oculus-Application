using UnityEngine;
using System.Collections;

public static class Utils {

	public static bool vectorApprximatelyTheSame(Vector3 a_vector1, Vector3 a_vector2)
	{
		bool v_areApprximatelyTheSame = true;
		float v_epsilon = 0.5f;
		if (Mathf.Abs(a_vector1.x - a_vector2.x) > v_epsilon) {
			v_areApprximatelyTheSame = false;
		}
		if (Mathf.Abs(a_vector1.y - a_vector2.y) > v_epsilon) {
			v_areApprximatelyTheSame = false;
		}
		if (Mathf.Abs(a_vector1.z - a_vector2.z) > v_epsilon) {
			v_areApprximatelyTheSame = false;
		}
			
		return v_areApprximatelyTheSame;
	}
}
