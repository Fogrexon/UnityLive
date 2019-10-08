Shader "Custom/Live/QuestBreak/Burst"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Lattice("LatticeNumber / 2", int) = 10
        _Progress("Progress", Range(0.0,1.0)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Progress;
            int _Lattice;

            float rand(float2 co){
				return frac(sin(dot(co.xy, float2(12.9898,78.233))) * 43758.5453);
			}

            float Ease(float t){
                return pow(t,0.7);
            }

            float4x4 eulerAnglesToRotationMatrix(float3 angles)
		  {
			  float ch = cos(angles.y); float sh = sin(angles.y); // heading
			  float ca = cos(angles.z); float sa = sin(angles.z); // attitude
			  float cb = cos(angles.x); float sb = sin(angles.x); // bank

			  // Ry-Rx-Rz (Yaw Pitch Roll)
			  return float4x4(
			  	ch * ca + sh * sb * sa, -ch * sa + sh * sb * ca, sh * cb, 0,
			  	cb * sa, cb * ca, -sb, 0,
			  	-sh * ca + ch * sb * sa, sh * sa + ch * sb * ca, ch * cb, 0,
			  	0, 0, 0, 1
			  );
		  }

            v2f vert (appdata v)
            {
                float3 vtx = v.vertex * 100;
                float2 uv = (floor(vtx.xy * _Lattice) + 0.5) / _Lattice;
                float r1 = rand(uv+1.0), r2 = rand(uv + 2.0), r3 = rand(uv + 2.0);
                float r = length(uv)/1.4142136;
                float p = max(_Progress - r,0.0) / (1 - r);
                float s = 1.0 - p;

                float4 basePos = float4(0.0,0.0, -pow(Ease(max(_Progress - r,0.0)),2.0)*3.0,0.0) + float4(uv,vtx.z,0.0);

                float4x4 mat = eulerAnglesToRotationMatrix((float3(r1,r2,r3)*6.283 - 3.1415) * p);

                float4 pos = mul(mat, float4(vtx - uv,0.0,1.0))*2.0 * s + basePos;

                v2f o;
                o.vertex = UnityObjectToClipPos(pos.xyz*0.01);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
