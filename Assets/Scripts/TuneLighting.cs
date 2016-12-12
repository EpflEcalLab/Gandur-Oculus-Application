using UnityEngine;
using System.Collections;

public class TuneLighting : MonoBehaviour {

	void OnEnable () {
		RenderSettings.ambientLight = Ressources.ambientColorSceneLM;
	}

	void OnDisable() {
		RenderSettings.ambientLight = Ressources.ambientColorDark;
	}
}
