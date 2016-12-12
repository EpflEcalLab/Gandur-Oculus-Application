Shader "Custom/DustMoteMod" {
	Properties {
		_Shape("Particle Shape", 2D) = "gray" {}
		_Noise("Noise Tex", 2D) = "gray" {}
		_DoShape("Do Shape Tex", Range(0,1)) = 0
		_LightColor ("Light Color", Color) = (0,1,0,1)
		_ShadowColor("Shadow Color", Color) = (0,0,1,1)
		_Speckles("Speckle Color", Color) = (.7,.7,1,1)
		_Blur ("Blur", Range(0,.5)) = .05
		_Emission("Emission", Range(0,1))=0 
		_Transparency("Transparency", Range(0,1))=0
		_FlashSpeed("Flash Speed", Range(0,1))=0
		_Threshold("Threshold", Range(0,1))=0.5
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)

		_StencilMask("Stencil Mask", Int) = 0
    }

SubShader
	{
		Tags {"RenderType"="Opaque" }

			LOD 400
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest Greater .01
		Cull Off

		Stencil 
		{
			Ref 1
			Comp equal
			Pass keep
			Fail keep
		}
		
			CGPROGRAM

			#pragma target 3.0 
			#pragma surface surf Lambert
			//Particule -> Lambert	


			float4 _LightColor;
			float _Blur;
			float _Emission;
			float _Transparency;
			sampler2D _Shape;
			float _DoShape;
			float4 _ShadowColor;
			float4 _Speckles;
			sampler2D _Noise; 
			float _FlashSpeed;
			float _Threshold;


//			//uses the first light created to simulate scattering through particles
//			half4 LightingLambert(SurfaceOutput s, half3 lightDir, half atten)
//			{ 
//				half4 c;  
//				c.rgb = s.Albedo;
//				c.a =lerp(0,s.Alpha, 0);
//				return c;
//			}
//

			struct Input 
			{
				float2 uv_Noise;
				float4 _Time;
				float4 color: Color;
				
			};

			//SurfaceOutputCustom -> SurfaceOutput
			void surf (Input IN, inout SurfaceOutput o)
			{
				 
				o.Alpha = IN.color.a;
				o.Albedo = _LightColor.rgb;
				
				//blend an opacity towards the center of the dust particle
				float dist = distance(IN.uv_Noise.xy, float2(.5,.5));
				if(dist<(.5-_Blur)){
					o.Alpha = o.Alpha*max(1.0*pow(dist/(.5-_Blur),2),(1-_Transparency));
					//add a shadow color as the particle fades
					o.Albedo = lerp(_ShadowColor.rgb, o.Albedo,o.Alpha);
				} 
				//rim blur
				else if(dist<.5){
					o.Alpha = o.Alpha*(.5-dist)/_Blur;
				
					
				}
				
				//mix in pinks
				o.Albedo = lerp(o.Albedo,_Speckles.rgb,tex2D(_Noise,(IN.uv_Noise.xy*.1+_Time*.01)).r);
				o.Alpha = 1;
				//use texture shape
				if(_DoShape)
					o.Alpha = o.Alpha*tex2D(_Shape,IN.uv_Noise.xy).r;
				
				//otherwise make a circle
				else{
					if(dist>=.5)
					o.Alpha = 0;
				}
			 	 //add flashes
			 	 o.Emission = lerp(0,_Emission, smoothstep(_FlashSpeed,1,o.Alpha));

			}
			
			ENDCG
		
	}

	Fallback "Diffuse"
}	