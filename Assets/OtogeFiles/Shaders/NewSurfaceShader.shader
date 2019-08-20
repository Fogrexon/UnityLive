Shader "Custom/NewSurfaceShader"
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
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        fixed4 _Color;
        fixed4 _Emission;
        float _BPM;


        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float t = saturate(sin(IN.worldPos.z * 3.0 + _Time.y * _BPM / 60.0 * 6.283184));
            o.Albedo = lerp(_Color, half4(0.0, 0.0, 0.0, 1.0), t).rgb;
            o.Emission = lerp(_Emission, half4(0.0, 0.0, 0.0, 1.0), t).rgb;
            o.Metallic = 0;
            o.Smoothness = 0;
            o.Alpha = 1.0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
