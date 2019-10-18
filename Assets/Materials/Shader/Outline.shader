Shader "Roystan/Toon"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}	
		[HDR]
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)  
		[HDR]
		_SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)  
		_Glossiness("Glossiness", Float) = 32   

		[HDR] 
		_RimColor("Rim Color", Color) = (1,1,1,1)  
		_RimAmount("Rim Amount", Range(0, 1)) = 0.716  

		_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
	}
	SubShader
	{
		Tags
		{
		"LightMode" = "ForwardBase"  //La première ligne demande la transmission de certaines données d'éclairage dans notre shader
		"PassFlags" = "OnlyDirectional"  //la seconde demande également de limiter ces données à la seule lumière directionnelle principale.
		}	

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"  
			#include "AutoLight.cginc"
			
			float4 _AmbientColor; 
			float _Glossiness;   
			float4 _SpecularColor;  
			float4 _RimColor;  
			float _RimAmount;  
			float _RimThreshold;
			//calculer la quantité de lumière reçue par la surface de la lumière directionnelle principale. 
			//La quantité de lumière est proportionnelle à la direction ou normale de la surface par rapport à la direction de la lumière.

			struct appdata  //Les normales dans appdata sont renseignées automatiquement
			{
				float3 normal : NORMAL;   
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;  
			};

			struct v2f  //les valeurs dans v2f doivent être renseignées manuellement dans le vertex shader
			{
				float3 worldNormal : NORMAL; 
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
				SHADOW_COORDS(2)
			};

			sampler2D _MainTex;			
			float4 _MainTex_ST;					
			
			v2f vert (appdata v)
			{
				v2f o;
				o.viewDir = WorldSpaceViewDir(v.vertex);  
				o.worldNormal = UnityObjectToWorldNormal(v.normal); 
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);	
				TRANSFER_SHADOW(o)
				return o;
			}
			
			float4 _Color;

			float4 frag (v2f i) : SV_Target
			{
				float4 sample = tex2D(_MainTex, i.uv);				
				float3 normal = normalize(i.worldNormal); 
				float NdotL = dot(_WorldSpaceLightPos0, normal); 
				float shadow = SHADOW_ATTENUATION(i);
				float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);
				float3 viewDir = normalize(i.viewDir);  

				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir); 
				float NdotH = dot(normal, halfVector); 

				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness); 
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity); 
				float4 specular = specularIntensitySmooth * _SpecularColor; 
				float4 rimDot = 1 - dot(viewDir, normal);  
				float rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimDot);		 	
				float4 rim = rimIntensity * _RimColor;   
				float4 light = lightIntensity * _LightColor0;   //multiplions notre valeur lightIntensity existante et la stockons dans un float4 
																//afin d'inclure la couleur de la lumière dans notre calcul.
																//_LightColor0est la couleur de la lumière directionnelle principale. 
				
				return _Color * sample * (_AmbientColor + light + specular + rim);
			}
			ENDCG
		}
			UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}