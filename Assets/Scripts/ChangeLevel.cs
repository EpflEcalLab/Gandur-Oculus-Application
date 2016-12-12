using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag != "Player")
			return;
		SceneManager.LoadScene ("Scenes/FINAL_SCENE_STATE_DELPH");
	}
}
