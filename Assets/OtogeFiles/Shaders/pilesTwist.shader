// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/OtogeEffects/pilesTwist"
{
    Properties
    {
        _Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Emission ("Emission", Color) = (1.0, 1.0, 1.0, 1.0)
        _BPM ("BPM", Float) = 170.1
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
                float3 wpos : TEXCOORD1;
            };

            half4 _Color;
            half4 _Emission;
            float _BPM;

            v2f vert (appdata v)
            {
                v2f o;
                float4 vert = v.vertex;
                float theta = vert.y*0.3 * sin(_Time.y*0.783) + cos(_Time.y*1.135);
                float2x2 rot = float2x2(cos(theta),-sin(theta),sin(theta),cos(theta)); 
                vert.xz = mul(rot, vert.xz);
                o.vertex = UnityObjectToClipPos(vert);
                o.uv = v.uv;
                o.wpos = mul (unity_ObjectToWorld, v.vertex).xyz;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float f = saturate(sin(i.uv.x*100)*0.5 + cos(i.uv.y*100)*0.5);
                fixed4 col = lerp(half4(0.0, 0.0, 0.0, 1.0), _Color + _Emission,f);//max(0.0, sin(i.wpos.z*5.0 + _Time.y * _BPM / 60.0 * 6.283184)));
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }

    }
}
