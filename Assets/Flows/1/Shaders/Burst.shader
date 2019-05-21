Shader "Custom/VJ/Burst"
{
  Properties 
  {
    _Face("Face", Float) = 0
    _Color("Color", Color) = (1,1,1,1)
    _Emission("Emission", Color) = (1,1,1,1)
  }
  SubShader
  {
    Tags { "Queue"="Transparent"}
    LOD 100
    Cull Off

    Pass {
      CGPROGRAM
      #pragma vertex vert
      #pragma geometry geom
      #pragma fragment frag

      #include "UnityCG.cginc"

      struct appdata
      {
        float4 vertex : POSITION;
        float2 uv     : TEXCOORD0;
        float3 normal : NORMAL;
      };

      struct v2g{
        float4 vertex:  POSITION;
        float2 uv   : TEXCOORD0;
        float3 normal : NORMAL;
      };

      struct g2f{
        float4 vertex : SV_POSITION;
        float3 color : TEXCOORD0;
        float3 normal : NORMAL;
      };

      float _Face;
      fixed4 _Color;
      fixed4 _Emission;


      v2g vert (appdata v)
      {
        v2g o;
        o.vertex = v.vertex;
        o.uv = v.uv.xy;
        o.normal = v.normal;
        return o;
      }

      [maxvertexcount(22)]
      void geom(triangle v2g v[3], inout TriangleStream<g2f> stream)
      {
        g2f o;
        o.color = float4(1.0,1.0,1.0,1.0);
        float3 norm;
        norm = o.normal = v[0].normal;

        float3 v1 = v[0].vertex + _Face * norm;
        float3 v2 = v[1].vertex + _Face * norm;
        float3 v3 = v[2].vertex + _Face * norm;
        o.vertex = UnityObjectToClipPos(v1);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(v2);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(v3);
        stream.Append(o);
        stream.RestartStrip();

        v1 = v[0].vertex + _Face * norm;
        v2 = v[1].vertex;
        v3 = v[1].vertex + _Face * norm;
        o.normal = normalize(cross(v3-v2, v1-v2));
        o.vertex = UnityObjectToClipPos(v1);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(v2);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(v3);
        stream.Append(o);
        stream.RestartStrip();

        v1 = v[0].vertex + _Face * norm;
        v2 = v[0].vertex;
        v3 = v[1].vertex;
        o.normal = normalize(cross(v3-v2, v1-v2));
        o.vertex = UnityObjectToClipPos(v1);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(v2);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(v3);
        stream.Append(o);
        stream.RestartStrip();

        v1 = v[1].vertex + _Face * norm;
        v2 = v[2].vertex;
        v3 = v[2].vertex + _Face * norm;
        o.normal = normalize(cross(v3-v2, v1-v2));
        o.vertex = UnityObjectToClipPos(v1);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(v2);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(v3);
        stream.Append(o);
        stream.RestartStrip();

        v1 = v[1].vertex + _Face * norm;
        v2 = v[1].vertex;
        v3 = v[2].vertex;
        o.normal = normalize(cross(v3-v2, v1-v2));
        o.vertex = UnityObjectToClipPos(v1);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(v2);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(v3);
        stream.Append(o);
        stream.RestartStrip();

        v1 = v[2].vertex + _Face * norm;
        v2 = v[0].vertex;
        v3 = v[0].vertex + _Face * norm;
        o.normal = normalize(cross(v3-v2, v1-v2));
        o.vertex = UnityObjectToClipPos(v1);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(v2);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(v3);
        stream.Append(o);
        stream.RestartStrip();

        v1 = v[2].vertex + _Face * norm;
        v2 = v[2].vertex;
        v3 = v[0].vertex;
        o.normal = normalize(cross(v3-v2, v1-v2));
        o.vertex = UnityObjectToClipPos(v1);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(v2);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(v3);
        stream.Append(o);
        stream.RestartStrip();

      }

      fixed4 frag(g2f i) : SV_TARGET
      {
        half nl = max(0, dot(i.normal, _WorldSpaceLightPos0.xyz));
        fixed4 col;
        col = fixed4(_Color.xyz * nl,1)+_Emission;
        return col;
      }
      ENDCG
    }
  }
}