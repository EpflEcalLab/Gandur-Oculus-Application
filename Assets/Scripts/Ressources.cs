using UnityEngine;
using System.Collections;

public static class Ressources {

	public static string 	stateMachine_nameState = "State";
	public static string 	stateMachine_reset = "Reset";
	public static int 		stateMachine_initState = 12;

	public static string 	shaderNameParams_SpotColor = "_TintColor";
	public static string 	shaderNameParams_LightUp = "_EmissionColor";

	public static string 	tagDark = "SurfaceDark";
	public static string 	tagWhite = "SurfaceWhite";

	public static int forward = 1;
	public static int backward = -1;

	public static Color ambientColorDark =  new Color(0,0,0);
	public static Color ambientColorSceneLM =  new Color(0.7686275f,0.7411765f,0.7411765f);



	public static KeyCode 	keyQuit = KeyCode.Q;
	public static KeyCode	keyReset = KeyCode.R;
	public static KeyCode	keyPause = KeyCode.P;
	public static KeyCode	keyForward = KeyCode.U;
	public static KeyCode	keyBackward =KeyCode.D;
	public static KeyCode	keyAction = KeyCode.Space;

	#region LIGHT
	public static string lightMeshPrefabs = "Light/LightMesh";
	public static string lightParticlePrefabs = "Light/Light";
	#endregion

	#region DISPLACMENT
	public static string left = "Left"; // Do not change it, link to config file
	public static string middle = "Middle"; // Do not change it, link to config file
	public static string right = "Right"; // Do not change it, link to config file
	#endregion

}
