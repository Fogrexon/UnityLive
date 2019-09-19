// Upgrade NOTE: upgraded instancing buffer 'props' to new syntax.

Shader "Custom/InstancingFirefly"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Emission ("Emission", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard vertex:vert
        #pragma instancing_options procedural:setup

        struct Input
        {
            float2 uv_MainTex;
        };

        struct FireflyData{
            float3 Position;
        };

        #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
        StructuredBuffer<FireflyData> _FireflyDataBuffer;
        #endif

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed4 _Emission;

        float3 _ObjectScale;

        

        void vert(inout appdata_full v)
        {
            #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            FireflyData fireflyData = _FireflyDataBuffer[unity_InstanceID];

            float3 pos = fireflyData.Position.xyz;
            float3 scl = _ObjectScale;

            float4x4 object2world = (float4x4)0;
            object2world._11_22_33_44 = float4(scl.xyz, 1.0);
            object2world._14_24_34 += pos.xyz;

            v.vertex = mul(object2world, v.vertex);
            v.normal = normalize(mul(object2world, v.normal));
            #endif
        }

        void setup()
        {

        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Emission = _Emission;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
