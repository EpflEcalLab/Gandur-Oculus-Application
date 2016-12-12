using UnityEngine;
using System.Collections;

public class SceneGameObjects : MonoBehaviour {

	private	GameObject m_matterScene1;
	private GameObject m_matterScene2;
	private GameObject m_lightScene1;
	private GameObject m_lightMatterScene;
	private GameObject m_darkFloorSceneLight;
	private GameObject m_frameMatterToLight;

	private GameObject m_splineUserDisplacementLeft;
	private GameObject m_splineUserDisplacementRight;
	private GameObject m_splineUserDisplacementMiddle;

	private GameObject m_stateMachine;

	/// ////////////////
	public static SceneGameObjects Instance {  // singleton
		get {
			return instance;
		}
	}

	private static SceneGameObjects instance;
	/// ////////////////

	void Awake()
	{
		if (instance) {
			DestroyImmediate (gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad (gameObject);

		m_stateMachine = GameObject.Find (SceneGameObjectsName.stateMachine);
		if (m_stateMachine == null) {
			Debug.LogError ("Can't find GameObject StateMachine ");
		}

		m_matterScene1 = m_stateMachine.transform.FindChild (SceneGameObjectsName.sceneMatter1).gameObject;
		if (m_matterScene1 == null) {
			Debug.LogError ("Can't find GameObject SceneMatter1 ");
		}
			
		m_matterScene2 = m_stateMachine.transform.FindChild (SceneGameObjectsName.sceneMatter2).gameObject;
		if (m_matterScene2 == null) {
			Debug.LogError ("Can't find GameObject SceneMatter2 ");
		}

		m_lightScene1 = m_stateMachine.transform.FindChild (SceneGameObjectsName.sceneLight1).gameObject;
		if (m_lightScene1 == null) {
			Debug.LogError ("Can't find GameObject SceneLight1 ");
		}

		m_lightMatterScene = m_stateMachine.transform.FindChild (SceneGameObjectsName.sceneLightMatter).gameObject;
		if (m_lightMatterScene == null) {
			Debug.LogError ("Can't find GameObject SceneL&M");
		}

		m_darkFloorSceneLight = m_stateMachine.transform.FindChild (SceneGameObjectsName.darkFloorLight).gameObject;
		if (m_darkFloorSceneLight == null) {
					Debug.LogError ("Can't find GameObject DarkFloor ");
		}

		m_frameMatterToLight =  m_stateMachine.transform.FindChild (SceneGameObjectsName.frameMatterToLight).gameObject;
		if (m_frameMatterToLight == null) {
			Debug.LogError ("Can't find GameObject FrameMatterToLight ");
		}

		m_splineUserDisplacementLeft = GameObject.Find (SceneGameObjectsName.splineUserDisplacementLeft);
		if (m_splineUserDisplacementLeft == null) {
					Debug.LogError ("Can't find GameObject UserDisplacementLeft ");
		}

		m_splineUserDisplacementRight = GameObject.Find (SceneGameObjectsName.splineUserDisplacementRight);
		if (m_splineUserDisplacementRight == null) {
					Debug.LogError ("Can't find GameObject UserDisplacementRight ");
		}

		m_splineUserDisplacementMiddle = GameObject.Find (SceneGameObjectsName.splineUserDisplacementMiddle);
		if (m_splineUserDisplacementMiddle == null) {
					Debug.LogError ("Can't find GameObject UserDisplacementMiddle ");
		}
	}

	#region IO
	public GameObject matterScene1
	{
		get {
			return m_matterScene1;
		}
	}

	public GameObject matterScene2
	{
		get {
			return m_matterScene2;
		}
	}

	public GameObject lightScene1
	{
		get {
			return m_lightScene1;
		}
	}

	public GameObject lightMatterScene
	{
		get {
			return m_lightMatterScene;
		}
	}

	public GameObject splineUserDisplacementLeft
	{
		get {
			return m_splineUserDisplacementLeft;
		}
	}

	public GameObject splineUserDisplacementRight
	{
		get {
			return m_splineUserDisplacementRight;
		}
	}

	public GameObject splineUserDisplacementMiddle
	{
		get {
			return m_splineUserDisplacementMiddle;
		}
	}

	public GameObject stateMachine
	{
		get {
			return m_stateMachine;
		}
	}

	public GameObject darkFloorSceneLight
	{
		get {
			return m_darkFloorSceneLight;
		}
	}

	public GameObject frameMatterToLight
	{
		get {
			return m_frameMatterToLight;
		}
	}
	#endregion
}
