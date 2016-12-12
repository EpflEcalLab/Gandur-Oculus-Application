using UnityEngine;
using System.Collections;

public static class SceneGameObjectsUtils {

	public static float getMyGlobalXPositionLeft(GameObject a_gameOjbect)
	{
		float v_position = (a_gameOjbect.transform.position.x - a_gameOjbect.transform.localScale.x / 2);
		return v_position;
	}
}
