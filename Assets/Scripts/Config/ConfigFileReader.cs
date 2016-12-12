using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class ConfigFileReader : MonoBehaviour
{

	/// ////////////////
	public static ConfigFileReader Instance {  // singleton
		get {
			return instance;
		}
	}

	private static ConfigFileReader instance;
	/// ////////////////
		
	private string m_userDisplacmentType;

	#region INIT

	void Awake ()
	{
		if (instance) {
			DestroyImmediate (gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad (gameObject);

		StartCoroutine (readConfigFile ());
	}

	#endregion

	#region LOGIC

	private IEnumerator readConfigFile ()
	{
		string v_filePath = System.IO.Path.Combine (Application.streamingAssetsPath, ConfigFileName.configFileNamePath);
		string v_result = "";

		if (v_filePath.Contains ("://")) {
			WWW www = new WWW (v_filePath);
			yield return www;
			v_result = www.text;
		} else {
			v_result = System.IO.File.ReadAllText (v_filePath);
		}

		readDisplacementType (v_result);

	}

	private void readDisplacementType (string a_results)
	{
		XmlTextReader v_xmlTextReader = new XmlTextReader (new StringReader (a_results));
		if (v_xmlTextReader.ReadToFollowing ("UserDisplacementType")) {
			m_userDisplacmentType = v_xmlTextReader.ReadElementContentAsString ();
		}
	}
	#endregion

	#region IO
	public string userDisplacementType
	{
		get {
			return m_userDisplacmentType;
		}
	}

	#endregion
}
