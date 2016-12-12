using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightSplineHandler : MonoBehaviour {

	public List<GameObject> listLightSpline = new List<GameObject>();

	[SerializeField]
	private int m_nbLightSplineAlongY = 6;
	[SerializeField]
	private int m_nbLightSplineAlongZ = 6;

	public bool m_launchLightAnimation = false;
	public bool m_reset = false;

	public Color m_sphereLightColor = new Color(1,1,1);
	public Color m_splineLightColor = new Color(0.5f, 0.5f, 0.5f);

	#region INITIALISATION
	// Use this for initialization
	void Start () {
		int v_count = 0;
		for (int y = 0; y < nbLightSplineAlongY; y++) {
			for (int z = 0; z < nbLightSplineAlongZ; z++) {
				GameObject v_emptyGO = new GameObject ();
				LightSpline v_lightSpline = v_emptyGO.AddComponent<LightSpline> ();
				v_emptyGO.name = "Light Spline Nb " + v_count.ToString ("000");
				v_emptyGO.transform.SetParent (gameObject.transform);
				Vector3 v_positionSpline = new Vector3 (0, y * 40 , z * 60);
				if (v_count == 0) {
					v_lightSpline.init (v_positionSpline, v_emptyGO);
				} else {
					v_lightSpline.init (v_positionSpline, v_emptyGO, listLightSpline [0].GetComponent<LightSpline>().listLightParticle);
				}
				listLightSpline.Add (v_emptyGO);
				v_count++;
			}
		}

		fitItToTheLightScene ();
	}

	void OnEnable()
	{
		
	}
	#endregion

	#region UPDATE
	void Update()
	{
		if (m_launchLightAnimation) {
			foreach (var lightSpline in listLightSpline) {
				lightSpline.GetComponent<LightSpline> ().changeColorLightParticle (m_sphereLightColor);
				lightSpline.GetComponent<LightSpline> ().changeColorSplineParticle (m_splineLightColor);
			}
			m_launchLightAnimation = false;
		}

		if (m_reset == true) {
			m_reset = false;
			resetColorToInitialPosition ();
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			m_launchLightAnimation = true;
		}

		if (Input.GetKeyDown (KeyCode.L)) {
			m_reset = true;
		}
	}
	#endregion

	#region LOGIC
	private void fitItToTheLightScene()
	{
		gameObject.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		gameObject.transform.Rotate (0, 90, 0);
		gameObject.transform.localPosition = new Vector3 (49, 0, 36);
	}

	private void resetColorToInitialPosition ()
	{
		foreach (var lightSpline in listLightSpline) {
			lightSpline.GetComponent<LightSpline> ().resetColorLightParticle ();
		}
	}
	#endregion

	#region IO
	public int nbLightSplineAlongZ
	{
		get{
			return m_nbLightSplineAlongZ;
		}
	}

	public int nbLightSplineAlongY
	{
		get{
			return m_nbLightSplineAlongY;
		}
	}
	#endregion
	

}
