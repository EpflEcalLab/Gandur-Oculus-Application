Shader "Volumetric Fog/Bump Diffuse Texture" {
	Properties {
		_MainTex ("Main", 2D) = "white" {}
		_BumpTex ("Bump", 2D) = "black" {}
		_FogNoiseTex ("Noise", 3D) = "black" {}
		_FogScaleBias ("Fog Scale Bias", Vector) = (1, 1, 1, 1)
		_FogDensity ("Fog Density", Range(0, 0.01)) = 0.001
		_FogColor ("Fog Color", Color) = (0.9, 1, 0.8, 1)
	}
	SubShader {
		Tags { "RenderType" = "Opaque" "LightMode" = "ForwardBase" }
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#pragma multi_compile DVF_MEDIUM DVF_HIGH
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "Core.cginc"
			
			uniform sampler2D _MainTex, _BumpTex;
			uniform float4 _MainTex_ST;
			
			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 view : TEXCOORD0;    // world space view
				float3 fogcrd : TEXCOORD1;  // 3d noise coordinate
				float2 tex : TEXCOORD2;
				float3 lit : TEXCOORD3;
			};
			v2f vert (appdata_tan v)
			{
				TANGENT_SPACE_ROTATION;
				
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.view = WorldSpaceViewDir(v.vertex);
				o.tex = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.fogcrd = v.vertex.xyz * _FogScaleBias.w + _FogScaleBias.xyz;
				o.lit = mul(rotation, ObjSpaceLightDir(v.vertex));
				return o;
			}
			float4 frag (v2f i) : SV_TARGET
			{
				float3 N = tex2D(_BumpTex, i.tex).xyz * 2 - 1;
				float3 L = normalize(i.lit);
				float3 tc = tex2D(_MainTex, i.tex).rgb;
				tc *= dot(N, L) * _LightColor0.rgb;
				return VFog(i.fogcrd, i.view, tc);
			}
			ENDCG
		}
	}
	FallBack Off
}