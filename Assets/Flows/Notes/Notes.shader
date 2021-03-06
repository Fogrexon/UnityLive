﻿Shader "Unlit/Notes"
{
    Properties
    {
        _MainColor("MainColor", Color) = (1.0, 1.0, 1.0, 1.0)
        _Dissolve("Dissolve Texture", 2D) = "white" {}
        _Threshold("Threshold", Float) = 0.6
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Zwrite On
        Blend SrcAlpha OneMinusSrcAlpha

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

            sampler2D _Dissolve;
            half4 _MainColor;
            float _Threshold;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;//TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                
                float2 delta = float2(0.0,_Time.y)*0.4;
                float d = abs(length((i.uv - float2(0.5,0.5))*2.0) - 0.7) + tex2D(_Dissolve, i.uv+delta).x*0.2;
                float d2 = d < _Threshold ? 1.0:0.0;

                fixed4 col = fixed4(_MainColor.xyz*1.5*(1.0 - d*3.0), d2);
                

                /*
                float d = 1.0 - abs(length((i.uv - float2(0.5,0.5))*2.0) - 0.5)*3.0;
                //float d2 = d < _Threshold ? 1.0:0.0;
                d = saturate(d);

                fixed4 col = fixed4(_MainColor.xyz*1.5*d, d);

                */
                
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
