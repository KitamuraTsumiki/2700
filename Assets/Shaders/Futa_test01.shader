Shader "Izumikairiku/Futa_test01" {
	Properties {
		_Al ("Albedo", 2D) = "white" {}
		_Al_Sub ("Albedo Substance", 2D) = "white" {}
		_N ("Normal", 2D) = "bump" {}
		_N_Sub ("Normal Substance", 2D) = "bump" {}
		_MS ("Metallic/Smooth", 2D) = "black" {}
		_MSO_Sub ("Metallic/Smooth/AO", 2D) = "black" {}

		_SubLerpVal ("Substance Lerp Value", Range(0,1)) = 0.5
		_AOLerpVal ("AO Lerp Value", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vert:vertex

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _Al;
		sampler2D _Al_Sub;
		sampler2D _N;
		sampler2D _N_Sub;
		sampler2D _MS;
		sampler2D _MSO_Sub;

		float _SubLerpVal;
		float _AOLerpVal;

		struct Input {
			float2 uv_Al;
			float2 uv2_Al_Sub;
		};

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)
		

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Set Metallic, Smoothness
			half4 ms_tiling = tex2D(_MS, IN.uv_Al);
			half4 mso_sub = tex2D(_MSO_Sub, IN.uv2_Al_Sub);

			o.Metallic = lerp(ms_tiling.r, mso_sub.r, _SubLerpVal);
			o.Smoothness = lerp(ms_tiling.g, 1 - mso_sub.g, _SubLerpVal);

			// Set Albedo
			fixed4 c_tiling = tex2D (_Al, IN.uv_Al);
			fixed4 c_sub = tex2D(_Al_Sub, IN.uv2_Al_Sub);
			fixed ao = lerp(1, mso_sub.b, _AOLerpVal);
			
			o.Albedo = lerp(c_tiling.rgb, c_sub.rgb, _SubLerpVal) * ao;

			// Set Normal
			fixed3 n_tiling = UnpackNormal(tex2D(_N, IN.uv_Al));
			fixed3 n_sub = UnpackNormal(tex2D(_N_Sub, IN.uv2_Al_Sub));

			o.Normal = lerp(n_tiling, n_sub, _SubLerpVal);

			// Set others
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
