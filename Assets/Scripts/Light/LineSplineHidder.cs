using UnityEngine;
using System.Collections;

public class LineSplineHidder : MonoBehaviour {

	public bool m_showLineSplineHidder = true;
	private GameObject m_cubeToHide;


	// Use this for initialization
	void Start () {
		m_cubeToHide = gameObject.transform.FindChild ("Cube").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_showLineSplineHidder == true && !m_cubeToHide.activeInHierarchy) {
			m_cubeToHide.SetActive (true);
		} else if (m_showLineSplineHidder == false && m_cubeToHide.activeInHierarchy) {
			m_cubeToHide.SetActive (false);
		}
	}
}
