Shader "Custom/Hill" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Col0 ("Inside Color", COLOR) = (1,0,0,0.5)
		_Col1 ("Outside Color", COLOR) = (0,0,1,0.5)
		_Fill("Ratio", Range(0.0,1.0)) = 0
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert alpha

		half4 _Col0;
		half4 _Col1;
		half _Fill;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			float2 r = IN.uv_MainTex;
			float l =  length(r);
			half4 c = l < _Fill / 2 ? _Col0 : _Col1;
			o.Albedo = c.rgb;
			o.Alpha = l < 0.5 ? c.a : 0;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
