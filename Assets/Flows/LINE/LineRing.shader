Shader "Custom/Live/Line/LineRing"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LineNumber("Lines", Float) = 30
        _Speed("Speed", Float) = 1.0
        _Move("Move", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float rand(float x)
            {
                int y = floor(x*10000);
                int seed = 439;
                return frac(sin(y + seed) * 43758.5453);
            }

            float3 randCol(float x)
            {
                float3 col;
                col.r = rand(x);
                col.g = rand(col.r);
                col.b = rand(col.g);
                return col;
            }

            float _LineNumber;
            float _Speed;
            float _Move;

            fixed4 circle(float2 uv)
            {
                float y = floor((uv.y+_Move) * _LineNumber) / _LineNumber;
                float dx = 1.0 - saturate(((rand(y) + 1.0 + _Time.y * _Speed - uv.x)%1.0)*2.0);
                return float4(randCol(y)*dx,1.0);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half4 col = circle(i.uv);
                
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
