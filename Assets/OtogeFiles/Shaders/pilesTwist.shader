// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/OtogeEffets/pilesTwist"
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
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.wpos = mul (unity_ObjectToWorld, v.vertex).xyz;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = lerp(half4(0.0, 0.0, 0.0, 1.0), _Color + _Emission,max(0.0, sin(i.wpos.z*5.0 + _Time.y * _BPM / 60.0 * 6.283184)));
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }

        Pass
    {
        Name "ShadowCaster"
        Tags { "LightMode"="ShadowCaster" }

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma multi_compile_shadowcaster
        #include "UnityCG.cginc"

        struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f {
            V2F_SHADOW_CASTER;
            float2 uv : TEXCOORD1;
        };

        sampler2D _MainTex;
        float4 _MainTex_ST;
        float _Cutout;

        v2f vert(appdata v)
        {
            v2f o;
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            TRANSFER_SHADOW_CASTER(o)
            return o;
        }

        float4 frag( v2f i ) : COLOR
        {
            fixed4 texcol = tex2D( _MainTex, i.uv );
            if (texcol.a < _Cutout)
            {
                discard;
                }
            SHADOW_CASTER_FRAGMENT(i)
        }
        ENDCG
    }
    }
}
