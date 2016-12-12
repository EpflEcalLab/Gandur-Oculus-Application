Shader "Volumetric Spot Light/Outside" {
	Properties {
		_NoiseTex ("Noise", 3D) = "white" {}
		_NoiseFollow ("Noise Follow", Vector) = (0, 0, 0, 0)
		_SpotColor ("Spot Color", Color) = (1, 1, 1, 1)
		_SpotPosition ("Spot Position", Vector) = (0, 0, 0, 0)
		_SpotAttenuation ("Spot Attenuation", Range(0, 16)) = 5
		_SpotAnglePower ("Spot Angle Power", Range(0.1, 8)) = 1.2
		_InvFade ("Soft Factor", Range(0.01, 3)) = 1.0
		_NoiseScale ("Noise Scale", Float) = 1
		_NoiseBias ("Noise Bias", Float) = 0
		_BeamColorTex ("Beam Color", 2D) = "white" {}
		_BeamIntensity ("Beam Intensity", Float) = 1
		_BeamMove ("Beam Move", Float) = 0.3
	}
	SubShader {
		Tags { "RenderType" = "Transparent" "IgnoreProjector" = "True" "Queue" = "Transparent" }
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Back
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment fragOutside
			#pragma multi_compile _ VSL_NOISE_FOLLOW VSL_COLOR_BEAM
			#pragma multi_compile _ VSL_SOFT_INTERSECTION
			#include "UnityCG.cginc"
			#include "Utils.cginc"
			ENDCG
		}
	}
	FallBack Off
}