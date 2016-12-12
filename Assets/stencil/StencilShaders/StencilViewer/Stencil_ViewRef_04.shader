Shader "Stencils/Viewers/Stencil_ViewRef_04"
{
	Properties
	{
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" "Queue"="Geometry+10" }		
		ZWrite off
		ZTest Always
		Stencil 
		{
			Ref 4
			Comp Equal
			Pass Keep
		}
		
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag 
			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"
			#include "AutoLight.cginc"
			float4 _Color;
			
			struct appdata 
			{
				float4 vertex : POSITION;
			};
			
			struct v2f 
			{
				//LIGHTING_COORDS
				float4 diff : COLOR0;
				float4 pos : SV_POSITION;
			};
			
			v2f vert(appdata_base v) 
			{
				v2f o;
				float ldir = normalize(ObjSpaceLightDir(v.vertex));
				float diffuse = dot(v.normal,ldir);
				o.diff = diffuse * _LightColor0;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}
			
			half4 frag(v2f i) : COLOR 
			{

				//LIGHTING_COORDS(idx1,idx2)
				//_Color *= i.diff;// * LIGHT_ATTENUATION(i)*2;
				return _Color;
			}

		ENDCG
		}
	}
}