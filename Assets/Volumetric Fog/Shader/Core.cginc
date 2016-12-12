#ifndef DVF_CORE_INCLUDED
#define DVF_CORE_INCLUDED

#ifdef DVF_MEDIUM
	#define COUNT 64
#endif
#ifdef DVF_HIGH
	#define COUNT 128
#endif

uniform sampler3D _FogNoiseTex;
uniform float4    _FogScaleBias;
uniform float     _FogDensity;
uniform float3    _FogColor;

float4 VFog (float3 coord, float3 view, float3 baseColor)
{
	float n = _FogDensity * length(view);
	float3 uvw = coord;
	float a = 1.0;
	float b = 0.0;
	for (int i = 0; i < COUNT; i++)
	{
		uvw += _FogScaleBias.w * view;
		float fog = tex3D(_FogNoiseTex, uvw).x;
		float x = 1.0 - fog * n;
		a *= x;
		b = lerp(1.0, b, x);
	}
	return float4(baseColor * a + _FogColor * b, 1.0);
}

#endif