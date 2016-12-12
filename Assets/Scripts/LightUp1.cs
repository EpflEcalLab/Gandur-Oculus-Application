using UnityEngine;
using System.Collections;

public class LightUp1 : MonoBehaviour {

	public Material lightMat;
	public bool lightChange = false;

	public bool isFinished = false;

	public float targetedColor;
	public float currentColor;
	public Color startColor;
	public float speedColorChange;

	void OnEnable () {
		
		InstantChange (startColor);
		currentColor = startColor.r;
		targetedColor = 92/256f;
		lightChange = false;
	}


	void Update () {

		if (lightChange) {
			
			IncreaseLightMaterial ();
			//DecreaseLightMaterial ();
		}
	}

	public void InstantChange(Color color){
		lightMat.color = color;
	}

	public void IncreaseLightMaterial(){
	
		if (currentColor >= targetedColor) {
			lightChange = false;
		} else {
			currentColor += speedColorChange * Time.deltaTime;
		}
		InstantChange (new Color (currentColor, currentColor, currentColor));
	
	}

	public void DecreaseLightMaterial(){

		if (targetedColor <= 0f) {
			lightChange = false;
			isFinished = true;
		} else {
			currentColor -= speedColorChange * Time.deltaTime;
		}
		InstantChange (new Color (currentColor, currentColor, currentColor));

	}
}
