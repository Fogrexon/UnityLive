Shader "Custom/Rings" {
	Properties{
		_Radius("Radius",Float) = 2
		_Width("Width", Float) = 0.1
		_RingColor("RingColor", Color) = (1.0,1.0,1.0,1.0)

	}
	SubShader{
		Tags { "RenderType" = "Transparent" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard alpha
		#pragma target 3.0

		struct Input {
			float3 worldPos;
		};

		float _Radius;
		float _Width;
		fixed4 _RingColor;

		void surf(Input IN, inout SurfaceOutputStandard o) {
			float dist = abs(distance(fixed2(0,0), IN.worldPos.xz) - _Radius);
			
			o.Albedo = _RingColor;
			if ( dist < _Width ) {
				o.Alpha = 1.0;
			} else {
				o.Alpha = 0.0;
			}
		}
		ENDCG
	}
		FallBack "Diffuse"
}