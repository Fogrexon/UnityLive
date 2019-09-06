Shader "Custom/OtogeEffects/DepthEffect"
{
    Properties {
        _MainTex("MainTex", 2D) = ""{}
        _Color("FogColor", Color) = (1.0, 0.0, 0.0, 1.0)
        _Base("Base", Float) = 0.2
    }

    SubShader
    {
        Cull Off
        ZTest Always
        ZWrite Off
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _CameraDepthTexture;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            float _Base;

            float rand(float2 co){
                return frac(sin(dot(co.xy ,float2(12.9898,78.233))) * 43758.5453);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed depth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture,i.uv));
                
                fixed4 texcol = tex2D(_MainTex, i.uv);

                float b = _Base*0.4 + sin(i.uv.x*100.0 + i.uv.y*100.0)*0.6;

                fixed4 col = texcol * b + lerp(_Color, texcol, pow(saturate(-0.2 + depth*50), 2)) * (1 - b);

                return col;
            }
            ENDCG
        }
        
    }
}