using UnityEngine;
using System.Collections.Generic;

public class Demo : MonoBehaviour
{
	[Range(-1f, 1f)] public float m_FogSpeedX = 0f;
	[Range(-1f, 1f)] public float m_FogSpeedY = 0.5f;
	[Range(-1f, 1f)] public float m_FogSpeedZ = 0f;
	[Range(0.0001f, 0.005f)] public float m_FogDensity = 0.001f;
	public enum EColor { White = 0, Brown, LightBlue, LightGreen };
	public EColor m_FogColor = EColor.White;
	public enum EQuality { Medium = 0, High };
	public EQuality m_FogQuality = EQuality.High;
	public GameObject[] m_FogObjects;
	private Noise3D m_NoiseTexture = new Noise3D ();
	private Color[] m_ColorLib = new Color[]{ 
		Color.white,
		new Color (0.9f, 1f, 0.8f),
		new Color (0f, 0.5f, 1f),
		new Color (0.5f, 1f, 0.5f)
	};

    private void Start ()
	{
		m_NoiseTexture.Create (64, 16);
		for (int i = 0; i < m_FogObjects.Length; i++)
		{
			MeshRenderer rd = m_FogObjects[i].GetComponent<MeshRenderer>();
			rd.material.SetTexture ("_FogNoiseTex", m_NoiseTexture.Get ());
		}
	}
	private void Update ()
    {
		for (int i = 0; i < m_FogObjects.Length; i++)
		{
			Vector4 scaleBias = new Vector4 (m_FogSpeedX * Time.realtimeSinceStartup, m_FogSpeedY * Time.realtimeSinceStartup, m_FogSpeedZ * Time.realtimeSinceStartup, 0.0008f);
			MeshRenderer rd = m_FogObjects[i].GetComponent<MeshRenderer>();
			rd.material.SetVector ("_FogScaleBias", scaleBias);
			rd.material.SetFloat ("_FogDensity", m_FogDensity);
			rd.material.SetColor ("_FogColor", m_ColorLib[(int)m_FogColor]);
			if (m_FogQuality == EQuality.Medium)
			{
				rd.material.EnableKeyword ("DVF_MEDIUM");
				rd.material.DisableKeyword ("DVF_HIGH");
			}
			if (m_FogQuality == EQuality.High)
			{
				rd.material.DisableKeyword ("DVF_MEDIUM");
				rd.material.EnableKeyword ("DVF_HIGH");
			}
		}
    }
	/*private void OnGUI ()
	{
		GUI.Box (new Rect (10, 10, 240, 25), "Volumetric Fog Demo");
	}*/
}
