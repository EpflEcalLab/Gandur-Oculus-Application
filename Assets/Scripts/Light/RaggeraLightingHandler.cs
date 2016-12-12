using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RaggeraLightingHandler : MonoBehaviour {

	public bool m_launchLightAnimation = false;
	public bool m_reset = false;

	public Material m_raggeraBeam;
	public Material m_raggeraSun;

	public Color m_sunColor = new Color (0.9f, 0.9f, 0.9f);
	public Color m_beamColor = new Color (0.7f, 0.7f, 0.7f);

	private GameObject m_pointLight;

	private float m_lenghtInSecondAnimation = 4f;


	void OnEnable()
	{
		m_pointLight = gameObject.gameObject.transform.FindChild ("PointLightRaggera").gameObject;
		m_pointLight.SetActive (false);
		resetColorToInitialPosition ();
	}

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (m_launchLightAnimation) {
			m_launchLightAnimation = false;
			Sequence v_sequence = DOTween.Sequence ();
			v_sequence.Insert(0,m_raggeraBeam.DOColor (m_sunColor, Ressources.shaderNameParams_LightUp, m_lenghtInSecondAnimation));
			v_sequence.Insert(0,m_raggeraSun.DOColor (m_beamColor, "_Color", m_lenghtInSecondAnimation));
			v_sequence.Insert(0,m_raggeraSun.DOColor(m_sunColor, Ressources.shaderNameParams_LightUp, m_lenghtInSecondAnimation));
			v_sequence.AppendCallback (() => {
				m_pointLight.SetActive (true);
			});
			v_sequence.Play ();
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

	private void resetColorToInitialPosition ()
	{
		m_pointLight.SetActive (false);
		m_raggeraBeam.SetColor( "_EmissionColor", new Color (0f, 0f, 0f));
		m_raggeraSun.SetColor("_EmissionColor",new Color (0f, 0f, 0f));
		m_raggeraSun.SetColor ("_Color", new Color (0f, 0f, 0f));
	}
}
