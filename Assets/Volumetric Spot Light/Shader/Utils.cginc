// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

#ifndef VSL_UTILS_CGINC
#define VSL_UTILS_CGINC

// soft intersection ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
uniform sampler2D_float _CameraDepthTexture;
uniform float _InvFade;

float VSL_SoftIntersection (in float4 projpos)
{
	float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(projpos)));
	float partZ = projpos.z;
	return saturate(_InvFade * (sceneZ - partZ));
}

// noise density //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
uniform sampler3D _NoiseTex;
uniform float4 _NoiseFollow;
uniform float _NoiseScale, _NoiseBias;

float VSL_NoiseFollow (in float3 v)
{
	float3 uvw = normalize(v);
	uvw.xy += _Time.y * _NoiseFollow.x;
	uvw.z += _Time.y * _NoiseFollow.y;
	return tex3D(_NoiseTex, uvw).x;
}

// core ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
uniform float3 _SpotColor, _SpotPosition;
uniform float _SpotAttenuation, _SpotAnglePower;
uniform sampler2D _BeamColorTex;
uniform float _BeamIntensity, _BeamMove;

struct v2f
{
	float4 pos : POSITION;
	float3 wldnor : TEXCOORD0;   // world space normal
	float3 wldpos : TEXCOORD1;   // world space position
	float4 wldview : TEXCOORD2;  // world space view (xyz), clip plane alpha fading (a)
#ifdef VSL_SOFT_INTERSECTION
	float4 projpos : TEXCOORD3;
#endif
#ifdef VSL_NOISE_FOLLOW
	float3 uvw : TEXCOORD4;
#endif
#ifdef VSL_COLOR_BEAM
	float2 uv : TEXCOORD5;   // sphere mapping
#endif
};
v2f vert (appdata_base v)
{
	v2f o;
	o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	o.wldnor = mul((float3x3)unity_ObjectToWorld, SCALED_NORMAL);
	o.wldpos = mul(unity_ObjectToWorld, v.vertex).xyz;
	o.wldview.xyz = WorldSpaceViewDir(v.vertex);
				
	// soft near-clip plane
	float4 vp = mul(UNITY_MATRIX_MV, v.vertex);
	o.wldview.w = (-vp.z - _ProjectionParams.y) / 0.5;
	o.wldview.w = min(o.wldview.w, 1);

#ifdef VSL_SOFT_INTERSECTION
	o.projpos = ComputeScreenPos(o.pos);
	COMPUTE_EYEDEPTH(o.projpos.z);
#endif
#ifdef VSL_NOISE_FOLLOW
	o.uvw = v.vertex.xyz * _NoiseScale + _NoiseBias;
#endif
#ifdef VSL_COLOR_BEAM
	float3 vn = mul(UNITY_MATRIX_MV, SCALED_NORMAL);
	vn = normalize(vn);
	vp = normalize(vp);
	float3 r = reflect(vp, vn);
	r.z += 1;
	float m = 0.5 * rsqrt(dot(r, r));
	o.uv = r.xy * m + 0.5;
#endif
	return o;
}
float4 fragOutside (v2f i) : COLOR
{
	float inten = distance(i.wldpos, _SpotPosition) / _SpotAttenuation;
	inten = 1 - saturate(inten);

	float3 V = normalize(i.wldview.xyz);
	float3 N = normalize(i.wldnor);
	inten *= pow(dot(N, V), _SpotAnglePower);
#ifdef VSL_SOFT_INTERSECTION
	inten *= VSL_SoftIntersection(i.projpos);
#endif
#ifdef VSL_NOISE_FOLLOW
	inten *= VSL_NoiseFollow(i.uvw);
#endif
#ifdef VSL_COLOR_BEAM
	float2 uv = i.uv;
	uv.x += _Time.y * _BeamMove;
	float4 bc = tex2D(_BeamColorTex, uv);
	inten *= bc.r;
	_SpotColor *= (bc.rgb * _BeamIntensity);
#endif
	return float4(_SpotColor, inten * i.wldview.w);
}
float4 fragInside (v2f i) : COLOR
{
	float inten = distance(i.wldpos, _SpotPosition) / _SpotAttenuation;
	inten = 1 - saturate(inten);

	float3 V = normalize(i.wldview.xyz);
	float3 N = normalize(i.wldnor);
	inten *= pow(abs(dot(N, V)), _SpotAnglePower);
#ifdef VSL_SOFT_INTERSECTION
	inten *= VSL_SoftIntersection(i.projpos);
#endif
#ifdef VSL_NOISE_FOLLOW
	inten *= VSL_NoiseFollow(i.uvw);
#endif
#ifdef VSL_COLOR_BEAM
	float2 uv = i.uv;
	uv.x += _Time.y * _BeamMove;
	float4 bc = tex2D(_BeamColorTex, uv);
	inten *= bc.r;
	_SpotColor *= (bc.rgb * _BeamIntensity);
#endif
	return float4(_SpotColor, inten * i.wldview.w);
}

#endif