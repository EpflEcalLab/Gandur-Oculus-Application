Thank you for downloading !
Dynamic Volumetric Fog offers GPU approach to simulate natural fog phenomenon in 3d environment.

How to setup ?
=> Fog depend on a 3d noise texture, so all you need to do is transfer the 3d noise texture to material.
   1. The script "Noise3D.cs" is a 3d noise generator, call "Create" function to generate a 3d noise texture.
   2. Then just call "SetTexture" transfer the 3d noise to material which applyed on the game object.
   3. There are 3 shaders in package:
      => "Volumetric Fog/Texture" is foggy textured shader.
	  => "Volumetric Fog/Diffuse Texture" is foggy lighted textured shader.
	  => "Volumetric Fog/Bump Diffuse Texture" is foggy lighted bump textured shader.
	  And yes of course you can easily integrate the foggy effect into any existing shaders in your own projects.
	  Just call "VFog" function in "Core.cginc".
   
=> Please open "Demo.unity" to see the effect.
   1. The reference script "Demo.cs" is a demonstration about how to use effect.
   2. In script "Demo.cs" we use an array "m_FogObjects" to hold all "foggy objects" and transfer all necessary parameters to their material.

=> Performance impact control.
   1. In script "Demo.cs" there is "m_FogQuality", you can select "Medium" or "High"
   2. However this is just a high-level quality control, you can modify "COUNT" in "Core.cginc" to accurate control performance impact.
   3. Larger value of "COUNT" means better quality and larger performance impact, choose your own need.
   
If you like it, please give us a 5 star review on asset store. We will keep moving !
Any suggestion or improvement you want, please contact qq_d_y@163.com.