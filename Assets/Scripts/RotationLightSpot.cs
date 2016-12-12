using UnityEngine;
using System.Collections;

using DG.Tweening;

public class RotationLightSpot : MonoBehaviour {
	
	public Renderer quad0;
	public Renderer quad1;
	public GameObject user;

	[Header("FollowUser")]
	[Space(10)]
	public float shiftRotationZ;
	public float reverse;

	[Header("Behavior")]
	[Space(10)]
	public bool rotation;

	private float currentColor = 0;
	private float currentIntensity = 0;
	private const float targetedColor = 0.5f;
	private const float speedLightUp = 0.25f;
	private const float intensity = 8f;
	private const float speedIntensity = 1f;
	private const float timeRotation = 10f;
	private const float angleRotation = -37f;
	private const float alphaColorBase = 45f/256f;
	private Quaternion initRot;
	private Tween v_mytWEEN;
	private Tween v_lookAt;

	void Awake(){
		initRot = this.gameObject.transform.rotation;
	}

	void OnEnable () {
		
		quad0.material.SetColor (Ressources.shaderNameParams_SpotColor, new Color (currentColor, currentColor, currentColor,alphaColorBase));
		quad1.material.SetColor (Ressources.shaderNameParams_SpotColor, new Color (currentColor, currentColor, currentColor,alphaColorBase));

		this.gameObject.transform.rotation = initRot;
		if (rotation) {
			v_mytWEEN = this.gameObject.transform.DOLocalRotate (new Vector3 (angleRotation, 0, 0), timeRotation, RotateMode.LocalAxisAdd).SetLoops (-1, LoopType.Yoyo);
			v_mytWEEN.Play ();
		}
	}
	void OnDisable () {
		if(rotation)
		v_mytWEEN.Kill();
	}
	// Update is called once per frame
	void Update () {
		
		Vector3 difference = user.transform.position - transform.position;
		float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(90.0f, 0.0f, rotationZ*reverse+shiftRotationZ);

		if (currentColor < targetedColor) {
			currentColor += speedLightUp * Time.deltaTime;

			quad0.material.SetColor (Ressources.shaderNameParams_SpotColor, new Color (currentColor, currentColor, currentColor,alphaColorBase));
			quad1.material.SetColor (Ressources.shaderNameParams_SpotColor, new Color (currentColor, currentColor, currentColor,alphaColorBase));
		}
		if (currentIntensity < intensity) {
			currentIntensity += speedIntensity * Time.deltaTime;
			this.gameObject.GetComponent<Light> ().intensity = currentIntensity;
		}
	}


}
