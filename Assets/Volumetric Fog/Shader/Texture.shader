Shader "Volumetric Fog/Texture" {
	Properties {
		_MainTex ("Main", 2D) = "white" {}
		_FogNoiseTex ("Noise", 3D) = "black" {}
		_FogScaleBias ("Fog Scale Bias", Vector) = (1, 1, 1, 1)
		_FogDensity ("Fog Density", Range(0, 0.01)) = 0.001
		_FogColor ("Fog Color", Color) = (0.9, 1, 0.8, 1)
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		Pass {
			Cull Off
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DVF_MEDIUM DVF_HIGH
			#include "UnityCG.cginc"
			#include "Core.cginc"
			
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			
			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 view : TEXCOORD0;    // world space view
				float3 fogcrd : TEXCOORD1;  // 3d noise coordinate
				float2 tex : TEXCOORD2;
			};
			v2f vert (appdata_tan v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.view = WorldSpaceViewDir(v.vertex);
				o.tex = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.fogcrd = v.vertex.xyz * _FogScaleBias.w + _FogScaleBias.xyz;
				return o;
			}
			float4 frag (v2f i) : SV_TARGET
			{
				float3 tc = tex2D(_MainTex, i.tex).rgb;
				return VFog(i.fogcrd, i.view, tc);
			}
			ENDCG
		}
	}
	FallBack Off
}
