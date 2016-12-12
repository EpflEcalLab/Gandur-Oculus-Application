using UnityEngine;
using System.Collections;

public class ReflectParticule : MonoBehaviour {
	
	public ParticleSystem emitterDarkParticule;
	public ParticleSystem emitterWhiteParticule;
	public Light spotLight;
	public GameObject user;
	public bool activateEmitter;
	private enum enumColor {Dark, White, Gray};
	private enumColor currentColor;

	private bool sizeChanging; 
	private bool intensityChanging; 
	private float nextSizeSpot;
	private float nextIntensitySpot;
	private float factorSizeSpot;
	private float factorIntensitySpot;

	private bool underChange = false;

	private const float initSizeSpot = 10f;
	private const float initIntensitySpot = 8f;

	private const float minSizeSpot = 10f;
	private const float midSizeSpot = 10f;
	private const float maxSizeSpot = 10f;

	private const float minIntensitySpot = 3f;
	private const float midIntensitySpot = 8f;
	private const float maxIntensitySpot = 8f;

	private const float changeSizeSpotSpeed = 0.05f;
	private const float changeIntensitySpotSpeed = 0.05f;

	// Use this for initialization
	void OnEnable () {
		activateEmitter = false;
		underChange = false;
		emitterDarkParticule.Stop ();
		emitterWhiteParticule.Stop ();

		factorSizeSpot = spotLight.spotAngle = initSizeSpot;
		factorIntensitySpot = spotLight.intensity = initIntensitySpot;

		nextSizeSpot = initSizeSpot;
		sizeChanging = false;
		intensityChanging = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (sizeChanging) {
			//sizeChangeInstante ();
			//intensityChangeInstante ();
		}
		if (intensityChanging){
			intensityChangeInstante();
		}
	}

	void OnTriggerEnter(Collider other){
		
		if (other.tag == Ressources.tagDark) {
			currentColor = enumColor.Dark;
			ChangeOccurs ();
			emitterDarkParticule.gameObject.transform.localPosition = new Vector3(0f,0f,Mathf.Max(2f,Mathf.Abs( other.gameObject.transform.position.x - user.transform.position.x)/4));
		} else if(other.tag == Ressources.tagWhite) {
			currentColor = enumColor.White;
			ChangeOccurs ();
			emitterWhiteParticule.gameObject.transform.localPosition = new Vector3(0f,0f, Mathf.Max(2f,Mathf.Abs( other.gameObject.transform.position.x - user.transform.position.x)/4));
		}

	}
	void OnTriggerStay(Collider other){
		if (!activateEmitter) {
			ChangeOccurs ();
		} 
		if (other.tag == Ressources.tagWhite) {
			emitterWhiteParticule.gameObject.transform.localPosition = new Vector3 (0f, 0f, Mathf.Max (2f, Mathf.Abs (other.gameObject.transform.position.x - user.transform.position.x) / 4));
			if (currentColor != enumColor.White || emitterWhiteParticule.isStopped) {
				currentColor = enumColor.White;
				ChangeOccurs ();

				underChange = true;
			}
		
		} else if (other.tag == Ressources.tagDark && !underChange) {
			emitterDarkParticule.gameObject.transform.localPosition = new Vector3 (0f, 0f, Mathf.Max (2f, Mathf.Abs (other.gameObject.transform.position.x - user.transform.position.x) / 4));
			if (currentColor != enumColor.Dark || emitterDarkParticule.isStopped) {
				currentColor = enumColor.Dark;
				ChangeOccurs ();
			}
		} 

	}
	void OnTriggerExit(Collider other){


		if (other.tag == Ressources.tagDark) {
			currentColor = enumColor.Gray;
			ChangeOccurs ();

		} else if(other.tag == Ressources.tagWhite) {
			currentColor = enumColor.Gray;
			ChangeOccurs ();

			underChange = false;
		}
	}

	private void ChangeOccurs(){
		switch (currentColor){
		case enumColor.Dark:
			sizeChanging = true;
			intensityChanging = true;
			nextSizeSpot = minSizeSpot;
			nextIntensitySpot = minIntensitySpot;
			if (activateEmitter && emitterDarkParticule.gameObject.transform.localPosition.z<15f) {
				emitterDarkParticule.randomSeed = (uint)Random.Range (uint.MinValue, uint.MaxValue);
				emitterDarkParticule.Play ();
				emitterWhiteParticule.Stop ();
			} else {
				emitterDarkParticule.Stop ();
				emitterWhiteParticule.Stop ();
			}
			break;
		case enumColor.Gray:
			sizeChanging = true;
			intensityChanging = true;
			nextSizeSpot = midSizeSpot;
			nextIntensitySpot = midIntensitySpot;
			emitterDarkParticule.Stop ();
			emitterWhiteParticule.Stop ();
			break;
		case enumColor.White:
			sizeChanging = true;
			intensityChanging = true;
			nextSizeSpot = maxSizeSpot;
			nextIntensitySpot = maxIntensitySpot;
			if (activateEmitter && emitterWhiteParticule.gameObject.transform.localPosition.z<10f) {
				emitterWhiteParticule.randomSeed = (uint) Random.Range (uint.MinValue, uint.MaxValue);
				emitterDarkParticule.Stop ();
				emitterWhiteParticule.Play ();
			}else {
				emitterDarkParticule.Stop ();
				emitterWhiteParticule.Stop ();
			}
			break;
		}
	}

	private void sizeChangeInstante(){
		
		spotLight.spotAngle = nextSizeSpot;
		sizeChanging = false;

	}
	private void intensityChangeInstante(){

		spotLight.intensity = nextIntensitySpot;
		intensityChanging = false;
	}	
	private void intensityChangeSlowly(){
		spotLight.intensity = factorIntensitySpot;
		if (factorIntensitySpot > nextIntensitySpot) {
			factorIntensitySpot -= changeIntensitySpotSpeed;
			if (factorIntensitySpot < nextIntensitySpot)
				intensityChanging = false;
		} else if (factorIntensitySpot < nextIntensitySpot) {
			factorIntensitySpot += changeIntensitySpotSpeed;
			if (factorIntensitySpot > nextIntensitySpot)
				intensityChanging = false;
		} else {
			intensityChanging = false;
		}

	}
	private void sizeChangeSlowly(){
		spotLight.spotAngle = factorSizeSpot;
		if (factorSizeSpot > nextSizeSpot) {
			factorSizeSpot -= changeSizeSpotSpeed;
			if (factorSizeSpot < nextSizeSpot)
				sizeChanging = false;
		} else if (factorSizeSpot < nextSizeSpot) {
			factorSizeSpot += changeSizeSpotSpeed;
			if (factorSizeSpot > nextSizeSpot)
				sizeChanging = false;
		} else {
			sizeChanging = false;
		}

	}

}
