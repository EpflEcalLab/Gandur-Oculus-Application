using UnityEngine;
using System.Collections;
using DG.Tweening;

public class UserSplineAnimator : SplineAnimator {

	Sequence m_sequence;

	public float m_previousPassedTime = 0;

	bool m_endKeyDownU = false;
	bool m_endKeyDownD = false;
	bool m_stunByReset = false;
	int m_nbOfFrameToFinalizeAnimation = 20;
	int m_maxNumberOfFrameTOFinalizeAnimation = 20;


	public override void FixedUpdate( )
	{
		//Nothing to do
	}

	public void Update( )
	{
		updateSpeedValue ();

		m_previousPassedTime = passedTime;

		#region ALGO FOR EASING MOVING USER -> to be improved...
		if (Input.GetKey (Ressources.keyForward)) {
			passedTime += Time.deltaTime * speed;
			m_endKeyDownU = false;
			m_endKeyDownD = false;
			m_nbOfFrameToFinalizeAnimation = m_maxNumberOfFrameTOFinalizeAnimation;
		}

		if (Input.GetKeyUp (Ressources.keyForward)) {
			passedTime += Time.deltaTime * speed;
			m_endKeyDownU = true;
			m_nbOfFrameToFinalizeAnimation = m_maxNumberOfFrameTOFinalizeAnimation;
		}
			
		if (Input.GetKey (Ressources.keyBackward)) {
			passedTime -= Time.deltaTime * speed;
			m_endKeyDownD = false;
			m_endKeyDownU = false;
		}

		if (Input.GetKeyUp (Ressources.keyBackward)) {
			passedTime -= Time.deltaTime * speed;
			m_endKeyDownD = true;
			m_nbOfFrameToFinalizeAnimation = m_maxNumberOfFrameTOFinalizeAnimation;
		}


		if (m_endKeyDownU == true && m_nbOfFrameToFinalizeAnimation > 0) {
			passedTime += Time.deltaTime * speed * m_nbOfFrameToFinalizeAnimation / m_maxNumberOfFrameTOFinalizeAnimation;
			m_nbOfFrameToFinalizeAnimation--;
		}

		if (m_endKeyDownD == true && m_nbOfFrameToFinalizeAnimation > 0) {
			passedTime -= Time.deltaTime * speed * m_nbOfFrameToFinalizeAnimation / m_maxNumberOfFrameTOFinalizeAnimation;
			m_nbOfFrameToFinalizeAnimation--;
		}
		#endregion

		if (canUserMove (passedTime)) {
			Vector3 v_newPos = spline.GetPositionOnSpline (WrapValue (passedTime + offSet, 0f, 1f, wrapMode));
			transform.position = v_newPos;

			if (passedTime >= 1) {
				passedTime -= 1; 
			}
		} else {
			passedTime = m_previousPassedTime;
		}

	}
		

	private void updateSpeedValue()
	{
		int v_stateMachine = SceneGameObjects.Instance.stateMachine.GetComponent<Animator> ().GetInteger ("State");

		switch (v_stateMachine) {
		case 12:

			break;
		case 0:
			
			break;
		case 2:
			
			break;
		case 4:
		case 3:
			
			break;
		case 7:
			
			break;
		default:
			
			break;
		}
	}

	private void onReset()
	{
		passedTime = 0;
		StartCoroutine (TimeOutReset());
	}

	private bool canUserMove(float a_passed)
	{
		if (a_passed >= 1) {
			passedTime -= 1; 
		}

		if (a_passed < 0) {
			passedTime = 0;
			return false;
		}

		if (m_stunByReset) {
			return false;
		}
		
		Vector3 v_newPosUser = spline.GetPositionOnSpline (WrapValue (a_passed + offSet, 0f, 1f, wrapMode));
		Vector3 v_oldPosUser = transform.position;

		if (v_newPosUser.x > v_oldPosUser.x) {
			return true;
		}

		bool v_userCanMove = true;
		int v_stateMachine = SceneGameObjects.Instance.stateMachine.GetComponent<Animator> ().GetInteger ("State");

		switch (v_stateMachine) {
		case 0:
			v_userCanMove = true;
			break;
		case 2:
			if (v_newPosUser.x < SceneGameObjectsUtils.getMyGlobalXPositionLeft(
				SceneGameObjects.Instance.matterScene2)) {
				v_userCanMove =  false;
			}
			break;
		case 4:
		case 3:
			if (v_newPosUser.x < SceneGameObjectsUtils.getMyGlobalXPositionLeft(
				SceneGameObjects.Instance.frameMatterToLight)) {
				v_userCanMove =  false;
			}
			break;
		case 7:
			if (v_newPosUser.x < SceneGameObjectsUtils.getMyGlobalXPositionLeft(
				SceneGameObjects.Instance.lightMatterScene)) {
				v_userCanMove = false;
			}
			break;
		default:
			v_userCanMove = true;
			break;
		}
		return v_userCanMove;
	}

	public void Reset() {
		onReset ();
	}


	IEnumerator TimeOutReset() {

		m_stunByReset = true;
		yield return new WaitForSeconds(0.3f);
		m_stunByReset = false;
	}
}
