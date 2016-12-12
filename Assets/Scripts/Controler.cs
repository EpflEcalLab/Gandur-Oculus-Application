using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Controler : MonoBehaviour {

	public Animator stateMachine;
	public MoveInstantUser resetScriptTrigger;
	public Camera oculus;
	private TransitionByAnim[] scripts ;
	private int currentLayerMask;

	void Start () {

		currentLayerMask = oculus.cullingMask;
		AppInitValue ();
		scripts =  FindObjectsOfType(typeof(TransitionByAnim)) as TransitionByAnim[];
	}

	void Update () {

		if (Input.GetKey (Ressources.keyQuit)) {
			AppQuit ();
		}

		if (Input.GetKey (Ressources.keyReset)) {
			AppReset ();
		}
		if (Input.GetKey (Ressources.keyPause)) {
			AppPaused ();
		}
		if (Input.GetKey (KeyCode.Alpha0)) {
			AppStatus ();
		}
	}

	public static void AppQuit(){
		
		AppInitValue ();

		#if UNITY_EDITOR

		UnityEditor.EditorApplication.isPaused = false;
		UnityEditor.EditorApplication.isPlaying = false;

		#else

		Application.Quit ();

		#endif

	}
	public void AppReset(){
		resetScriptTrigger.Reset ();
		stateMachine.SetTrigger (Ressources.stateMachine_reset);
		stateMachine.SetInteger (Ressources.stateMachine_nameState, Ressources.stateMachine_initState);
		foreach (TransitionByAnim i in scripts)
		{
			i.Reset ();
		}
		//TODO refine
		UserSplineAnimator temp = FindObjectOfType(typeof(UserSplineAnimator)) as UserSplineAnimator;
		temp.Reset ();
		oculus.cullingMask = 0;

		StartCoroutine (TimeOutReset());

	}

	public static void AppPaused(){

		#if UNITY_EDITOR

		UnityEditor.EditorApplication.isPaused = !UnityEditor.EditorApplication.isPaused;


		#endif

	}

	public static void AppInitValue(){

		RenderSettings.ambientLight = Ressources.ambientColorDark;

	}

	public static void AppStatus(){

		int stateMachine = SceneGameObjects.Instance.stateMachine.GetComponent<Animator> ().GetInteger ("State");
		Debug.Log ("State machine :" +stateMachine);
	}

	IEnumerator TimeOutReset() {

		yield return new WaitForSeconds(0.32f);

		oculus.cullingMask = currentLayerMask;
	}

		
}
