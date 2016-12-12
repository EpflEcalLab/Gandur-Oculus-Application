using UnityEngine;
using System.Collections;

public class LightUp : MonoBehaviour {

	[Header("Event")]
	public bool lightChange = false;
	public bool isFinished = false;
	[Space(10)]
	[Header("Colors settings")]
	public Material lightMat;
	public float targetedColor;
	public float currentColor;
	public Color startColor;
	public float speedColorChange;
	[Header("Behavior")]
	[Space(10)]
	public bool decreaseColor;
	public bool changeEmission;
	public bool startOnTrigger;

	void OnEnable () {
		
		InstantChange (startColor);
		currentColor = startColor.r;
	}


	void Update () {

		if (lightChange) {
			if(!decreaseColor)
				IncreaseLightMaterial ();
			else
				DecreaseLightMaterial ();
		}
	}

	public void InstantChange(Color color){
		lightMat.color = color;
		if(changeEmission)
			lightMat.SetColor (Ressources.shaderNameParams_LightUp,color);
	}

	public void IncreaseLightMaterial(){
	
		if (currentColor >= targetedColor) {
			currentColor = targetedColor;
		} else {
			currentColor += speedColorChange * Time.deltaTime;
		}
		InstantChange (new Color (currentColor, currentColor, currentColor));
	
	}

	public void DecreaseLightMaterial(){

		if (currentColor <= 0f) {
			currentColor = 0f;
			lightChange = false;
			isFinished = true;
		} else {
			currentColor -= speedColorChange * Time.deltaTime;
		}
		InstantChange (new Color (currentColor, currentColor, currentColor));

	}
	void OnTriggerEnter(Collider other){

		if (other.tag == "Player" && startOnTrigger){
			this.lightChange = true;
		}
	}
}
