using UnityEngine;
using System.Collections;

public class UserDisplacementType : MonoBehaviour {

	public enum DisplacementType{

		UNKNOWN,
		LEFT,
		MIDDLE,
		RIGHT
	}

	private DisplacementType m_userDisplacmentType = DisplacementType.LEFT;
	private Spline m_splineUserDisplacement;

	#region INIT
	void Awake()
	{
		LoadDesiredDisplacement ();
	}

	private void LoadDesiredDisplacement ()
	{
		setDesiredDisplacement (ConfigFileReader.Instance.userDisplacementType);
		setDesiredSplineDisplacement ();
	}

	private void setDesiredDisplacement(string a_displacementType)
	{
		if (a_displacementType == Ressources.left) {
			m_userDisplacmentType = DisplacementType.LEFT;
		} else if (a_displacementType == Ressources.middle) {
			m_userDisplacmentType = DisplacementType.MIDDLE;
		} else if (a_displacementType == Ressources.right) {
			m_userDisplacmentType = DisplacementType.RIGHT;
		} else {
			m_userDisplacmentType = DisplacementType.UNKNOWN;
		}
	}

	private void setDesiredSplineDisplacement ()
	{
		switch (m_userDisplacmentType) {
		case UserDisplacementType.DisplacementType.LEFT:
			m_splineUserDisplacement = SceneGameObjects.Instance.splineUserDisplacementLeft.GetComponent<Spline>();
			break;
		case UserDisplacementType.DisplacementType.RIGHT:
			m_splineUserDisplacement = SceneGameObjects.Instance.splineUserDisplacementRight.GetComponent<Spline>();
			break;
		case UserDisplacementType.DisplacementType.MIDDLE:
			m_splineUserDisplacement = SceneGameObjects.Instance.splineUserDisplacementMiddle.GetComponent<Spline>();
			break;
		default:
			m_splineUserDisplacement = SceneGameObjects.Instance.splineUserDisplacementLeft.GetComponent<Spline>();
			break;
		}
	}
	#endregion

	#region IO
public Spline splineUserDisplacement
	{
		get{
		return m_splineUserDisplacement;
		}
	}
	#endregion
}
