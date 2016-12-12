using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class LightSpline : MonoBehaviour {

	Spline m_splineForParticle;
	SplineMesh m_splineMesh;

	GameObject m_lightSpline;

	private List<GameObject> m_listLightParticle = new List<GameObject>();
	private List<GameObject> m_listControlPoint = new List<GameObject>();
	private List<YAnimator> m_yAnimator = new List<YAnimator>();

	private int m_numberOfPoints = 26;
	private int m_nbOfLightParticle = 10;
	private float m_initXInterval = 40;
	private float m_initYInterval = 15;

	private Vector3 m_positionLightSpline = new Vector3();
	private List<GameObject> m_associatedListLightParticle;
	private bool m_hasIsOwnSplineAnimator = true;

	[SerializeField]
	private Color m_resetColor = new Color(0,0,0);

	private float m_lenghtInSecondAnimation = 4f;

	#region INIT
	public void init(Vector3 a_positionLightSpline, GameObject a_parent)
	{
		createSplineAt (a_positionLightSpline, a_parent);
	}

	public void init(Vector3 a_positionLightSpline, GameObject a_parent, List<GameObject> a_associatedListLightParticle)
	{
		m_hasIsOwnSplineAnimator = false;
		m_associatedListLightParticle = a_associatedListLightParticle;

		createSplineAt (a_positionLightSpline, a_parent);
	}
	#endregion

	#region LOGIC INIT
	private void createSplineAt(Vector3 a_positionLightSpline, GameObject a_parent)
	{
		m_positionLightSpline = a_positionLightSpline;

		m_lightSpline = (GameObject)Instantiate (Resources.Load( Ressources.lightMeshPrefabs));
		m_lightSpline.transform.SetParent (a_parent.transform);
		m_splineForParticle = m_lightSpline.GetComponent<Spline> ();
		m_splineMesh = m_lightSpline.GetComponent<SplineMesh> ();
		m_splineMesh.spline = m_splineForParticle;
		m_splineMesh.segmentCount = 500;
		m_splineForParticle.updateMode = Spline.UpdateMode.DontUpdate;

		Material v_material = m_lightSpline.GetComponent<Renderer>().material;
		v_material.SetColor("_EmissionColor",new Color(0,0,0));

		createALightSplineParticleAt(a_positionLightSpline);
		createLightParticleOnSpline ();
	}
	#endregion

	#region UPDATE
	void Update()
	{
		updateLightParticlePositionAccordingToAssociatedList ();
	}
	#endregion

	#region INIT LOGIC
	private void createALightSplineParticleAt(Vector3 a_positionLightSplineParticle)
	{
		for (int i = 0; i < m_numberOfPoints; i++) {

			Vector3 v_positionControlPoint = a_positionLightSplineParticle;

			v_positionControlPoint.x = m_initXInterval * i;

			if (i % 2 == 0) {
				v_positionControlPoint.y += m_initYInterval;
			} else {
				v_positionControlPoint.y -= m_initYInterval;
			}

			GameObject v_controlPoint = new GameObject ();
			v_controlPoint.name = "ControlPoints nb "+ i.ToString("000");
			v_controlPoint.transform.SetParent (m_splineForParticle.transform);

			SplineControlNode v_splineNode = v_controlPoint.AddComponent<SplineControlNode> ();
			v_splineNode.GetTransform.position = v_positionControlPoint;

			m_listControlPoint.Add (v_controlPoint);
		}
	}

	private void createLightParticleOnSpline()
	{
		float v_offest = 0;
		for (int i = 0; i < m_nbOfLightParticle; i++) {
			GameObject v_lightParticle = (GameObject)Instantiate (Resources.Load (Ressources.lightParticlePrefabs));
			v_lightParticle.name = "Light Particle nb " + i.ToString ("000");
			v_lightParticle.transform.SetParent (m_splineForParticle.transform);

			if (m_hasIsOwnSplineAnimator) {
				LightSplineAnimator v_splineAnim = v_lightParticle.AddComponent<LightSplineAnimator> ();
				v_splineAnim.speed = 0.03f;
				v_splineAnim.wrapMode = WrapMode.Loop;
				v_splineAnim.spline = m_splineForParticle;
				v_splineAnim.offSet = v_offest;
				v_offest -= 0.1f;
			}

			m_listLightParticle.Add (v_lightParticle);
		}
	}
	#endregion

	#region EVENT

	#endregion

	#region LOGIC
	public void changeColorLightParticle(Color a_color)
	{
		launchAnimationChangeLightParticleColor (a_color);
	}

	public void changeColorSplineParticle(Color a_color)
	{
		launchAnimationChangeSplineColor (a_color);
	}

	public void resetColorLightParticle()
	{
		foreach (var lightParticle in m_listLightParticle) {
			Material v_material = lightParticle.GetComponent<Renderer>().material;
			v_material.SetColor("_SpecColor",m_resetColor);
			v_material.SetColor("_Color",m_resetColor);
		}

		Material v_materialSpline = m_lightSpline.GetComponent<Renderer>().material;
		v_materialSpline.SetColor("_EmissionColor", m_resetColor);
	}

	private void launchAnimationChangeLightParticleColor (Color a_color)
	{
		Sequence v_sequence = DOTween.Sequence ();
		foreach (var lightParticle in m_listLightParticle) {
			Material v_material = lightParticle.GetComponent<Renderer>().material;
			v_sequence.Insert(0, v_material.DOColor(a_color,"_SpecColor", m_lenghtInSecondAnimation));
			v_sequence.Insert(0, v_material.DOColor(a_color,"_Color", m_lenghtInSecondAnimation));
		}

		v_sequence.Play ();
	}

	private void launchAnimationChangeSplineColor (Color a_color)
	{
		Sequence v_sequence = DOTween.Sequence ();
		Material v_material = m_lightSpline.GetComponent<Renderer>().material;
		v_sequence.Insert(0,v_material.DOColor(a_color,"_EmissionColor", m_lenghtInSecondAnimation));

		v_sequence.Play ();
	}

	private void updateLightParticlePositionAccordingToAssociatedList()
	{
		if (m_associatedListLightParticle == null) {
			return;
		}
		if (m_associatedListLightParticle.Count != m_listLightParticle.Count) {
			return;
		}

		int v_counter = 0;
		//TODO improve by getting transform from parent
		foreach (var lightPart in m_listLightParticle) {

			Vector3 v_position = m_associatedListLightParticle [v_counter].transform.position;
			Vector3 v_positionLightSpline = Quaternion.AngleAxis(90, Vector3.up) * m_positionLightSpline;
			v_position.x += v_positionLightSpline.x * 0.1f;
			v_position.y += v_positionLightSpline.y * 0.1f;
			v_position.z += v_positionLightSpline.z * 0.1f;
			lightPart.transform.position = v_position;

			v_counter++;
		}
	}
	#endregion

	#region IO
	public List<GameObject> associatedListLightParticle
	{
		get {
			return m_associatedListLightParticle;
		}
		set{
			m_associatedListLightParticle = value;
		}
	}

	public List<GameObject> listLightParticle
	{
		get {
			return m_listLightParticle;
		}
	}
	#endregion
}
