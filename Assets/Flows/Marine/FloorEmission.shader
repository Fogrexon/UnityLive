Shader "Custom/VJ/FloorEmission"
{
    Properties
    {
        _MainTex ("NormalMap", 2D) = "white" {}
        _Threshold("Threshold", Float) = 0.9
    }
    SubShader
    {
        LOD 100

        Pass
        {
            Tags { "RenderType"="Opaque" "Queue"="Geometry"}
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
            float _Threshold;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half4 col;

                if(min(fmod(i.uv.x+0.01,1.0),i.uv.x)<0.01 || min(fmod(i.uv.y+0.01,1.0),i.uv.y)<0.01) col = half4(2.0,2.0,2.0,1.0);
                else if(dot(normalize(tex2D(_MainTex, i.uv*0.7)).xyz, normalize(float3(1.0,0.0,0.0)))>_Threshold) col =  half4(2.0,2.0,2.0,1.0);
                else col = half4(0.0,0.0,0.0,1.0);
                
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
