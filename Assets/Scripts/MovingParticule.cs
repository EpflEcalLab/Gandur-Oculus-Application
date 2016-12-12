using UnityEngine;
using System.Collections;

public class MovingParticule : MonoBehaviour {

	public int step;
	public GameObject particule;
	public GameObject showCube;
	public GameObject[] path;
	public LightUp changeColor;
	public GameObject maskCube;
	public GameObject spotLightOcculus;
	public Transition trans;
	private bool part1 = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (part1) {
			if (step <= 1)
				step = 2;
			if (step >= path.Length - 1) {
				step = path.Length - 1;
				//maskCube.SetActive (false);
			}
			if (step % 2 != 0 && (step - 2) >= 0) {

				maskCube.SetActive (false);
	
			} else {
		
				maskCube.SetActive (true);

			}
			//TODO Do fct to convert step = i => 0or1or2 = 0 / 3or4 = 1 ...
			switch (step) {
			case 12:
			case 11:
				showCube.transform.position = path [9].transform.position;
				break;
			case 10:
			case 9:
				showCube.transform.position = path [7].transform.position;
				break;
			case 8:
			case 7:
				showCube.transform.position = path [5].transform.position;
				break;
			case 6:
			case 5:
				showCube.transform.position = path [3].transform.position;
				break;
			case 4:
			case 3:
				showCube.transform.position = path [1].transform.position;
				break;
			case 2:
			case 1:
				showCube.transform.position = path [0].transform.position;
				break;
			case 0:
				showCube.transform.position = path [0].transform.position;
				break;
			}

			if (Input.GetKey (KeyCode.UpArrow)) {
				particule.transform.position = Vector3.MoveTowards (particule.transform.position, path [step].transform.position, 5f * Time.deltaTime);
				if (Vector3.Distance (particule.transform.position, path [step].transform.position) <= 0.01f) {
					step++;
				}
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				particule.transform.position = Vector3.MoveTowards (particule.transform.position, path [step - 1].transform.position, 5f * Time.deltaTime);
				if (Vector3.Distance (particule.transform.position, path [step - 1].transform.position) <= 0.01f) {
					step--;
				}
			}
			//particule.transform.position = Vector3.MoveTowards (particule.transform.position, path [step].transform.position, 0.05f);

			if (Vector3.Distance (particule.transform.position, path [path.Length - 1].transform.position) <= 0.01f) {
			
				particule.transform.localScale = new Vector3 (3, 3, 3);
				particule.transform.parent = path [path.Length - 1].transform;

				if (part1) {
					TransitiontoScene2 ();
				}
			} else {
				float distance = (Vector3.Distance (particule.transform.position, path [path.Length - 1].transform.position));
				if (distance < 1f)
					distance = 1;

				//particule.transform.localScale = new Vector3 (0.5f+1/distance*10, 0.5f+1/distance*10, 0.5f+1/distance*10);
				particule.transform.parent = null;

			}
		}
	}


	void TransitiontoScene2(){
		part1 = false;
		spotLightOcculus.SetActive (false);
		//trans.state = 2;
		changeColor.InstantChange (Color.white);
		changeColor.lightChange = true;
		//changeColor.lightIncrease = -1;
		changeColor.currentColor = 1;
	
	}

}
