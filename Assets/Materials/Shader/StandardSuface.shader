Shader "Custom/StandardSuface"
{
    Properties
    {
	_MyTexture("My texture", 2D) = "white" {}
	_MyNormalMap("My normal map", 2D) = "bump" {}  // Grey

	_MyInt("My integer", Int) = 2
	_MyFloat("My float", Float) = 1.5
	_MyRange("My range", Range(0.0, 1.0)) = 0.5

	_MyColor("My colour", Color) = (1, 0, 0, 1)    // (R, G, B, A)
	_MyVector("My Vector4", Vector) = (0, 0, 0, 0) // (x, y, z, w)
    }

    SubShader
    {
        Tags { "Queue" = "Geometry"
		"RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

		sampler2D _MyTexture;  //type de texture
		sampler2D _MyNormalMap;  

		int _MyInt;
		float _MyFloat;
		float _MyRange;
		half4 _MyColor;   //couleur (32 ou 16 bit généralement)
		float4 _MyVector;  //les vecteurs

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
