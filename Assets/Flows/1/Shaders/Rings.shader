Shader "Custom/Rings" {
	Properties{
		_Radius("Radius",Float) = 2
		_Width("Width", Float) = 0.1
		_Box("Box", Float) = 0.5

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
		float _Box;

		fixed4 box(Input IN, float c){
			if(abs(IN.worldPos.x) % _Box < _Width || abs(IN.worldPos.z) % _Box < _Width)
			{
				return fixed4(c,c,c,1);
			}
			return fixed4(0,0,0,1);
		}

		void surf(Input IN, inout SurfaceOutputStandard o) {
			float dist = distance(fixed3(0,0,0), IN.worldPos);
			dist -= _Time.y*_Radius*60/130*1.2;
			float radius = 2;
			float a = abs(dist) % _Radius;
			float b = abs( _Radius - a );
			/*if ( a < _Width || b < _Width) {
				o.Albedo = fixed4(1,1,1, 1);
			} else {*/
				float c = pow(a/_Radius,4) + pow(b/_Radius,4)-0.5;
				o.Emission = box(IN,max(c,0));
				o.Alpha = c;
			//}
		}
		ENDCG
	}
		FallBack "Diffuse"
}