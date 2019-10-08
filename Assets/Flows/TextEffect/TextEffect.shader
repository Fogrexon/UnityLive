Shader "Custom/Live/TextEffect/TextEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorDiff("ColorDifferenceSeed", Vector) = (0.0,0.0,0.0,0.0)
        _Diff("Difference", Float) = 0.05
        _Progress("Progress", Range(0.0,1.0)) = 0.0
        _Scale("Scale", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Transparent" }
        LOD 100
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float2 _ColorDiff;
            float _Diff;
            float _Progress;
            float _Scale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float rand(float2 co){
				return (frac(sin(dot(co.xy, float2(12.9898,78.233))) * 43758.5453)*2.0 - 1.0) * _Diff;
			}

            fixed4 frag (v2f i) : SV_Target
            {
                float2 c = _ColorDiff;
                float r1 = rand(c),r2 = rand(c+1),g1 = rand(c+2),g2 = rand(c+3),b1 = rand(c+4),b2 = rand(c+5);
                float2 uv = i.uv + float2(rand(i.uv),-abs(rand(i.uv+1))*10.0) / _Diff * 0.09 * _Progress;
                fixed4 red = tex2D(_MainTex,uv-float2(r1,r2));
                fixed4 green = tex2D(_MainTex,uv-float2(g1,g2));
                fixed4 blue = tex2D(_MainTex,uv-float2(b1,b2));
                fixed4 col = fixed4(red.r*red.a, green.g*green.a, blue.b*blue.a,max(red.a,max(green.a,blue.a))*(1.0-_Progress));
                // apply fogs
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
