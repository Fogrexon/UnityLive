Shader "Custom/OtogeEffects/Wireframe" {
  Properties {
    _MainTex ("Texture", 2D) = "white" {}

    [Header(Wireframe)]
    _Color ("Color", Color) = (1,1,1,1)
    _Width ("Width", Float) = 0.005
  }

  SubShader {
    

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

    // 0: wireframe pass
    Pass {
      Tags { "RenderType"="Transparent" "Queue"="Transparent" }
    LOD 100
      Cull Off
      Blend SrcAlpha OneMinusSrcAlpha
      ZWrite Off

      CGPROGRAM
      #pragma target 4.0
      #pragma vertex vert
      #pragma geometry geo
      #pragma fragment frag
      #pragma multi_compile_fog
      #include "UnityCG.cginc"

      struct appdata {
        float4 vertex : POSITION;
        float4 color : COLOR;
      };

      struct v2g {
        float4 vertex : POSITION;
        float4 color : TEXCOORD0;
      };

      struct g2f {
        float4 vertex : SV_POSITION;        
        float4 color  : TEXCOORD0;
        UNITY_FOG_COORDS(1)
      };

      float4 _Color;
      float _Width;
      
      v2g vert (appdata v) {
        v2g o;
        o.vertex = v.vertex;
        o.color = v.color;
        return o;
      }

      float3 delta(float2 v)
      {
        return float3(0,0,cos(v.x * 1000 + _Time.y )*0.0003 + sin(v.y * 1000 + _Time.y * 0.7)*0.0002);
      }

      [maxvertexcount(21)]
      void geo(triangle v2g v[3], inout TriangleStream<g2f> TriStream) {

        for (int i = 0; i < 3; i++) {
          v2g vb = v[(i + 0) % 3];
          v2g v1 = v[(i + 1) % 3];
          v2g v2 = v[(i + 2) % 3];

          float3 dir = normalize((v1.vertex.xyz + v2.vertex.xyz) * 0.5 - vb.vertex.xyz);

          g2f o;
          o.color  = _Color * v[0].color;

          o.vertex = UnityObjectToClipPos(float4(v1.vertex.xyz, 1));
          UNITY_TRANSFER_FOG(o,o.vertex);
          TriStream.Append(o);

          o.vertex = UnityObjectToClipPos(float4(v2.vertex.xyz, 1));
          UNITY_TRANSFER_FOG(o,o.vertex);
          TriStream.Append(o);

          o.vertex = UnityObjectToClipPos(float4(v2.vertex.xyz + dir * _Width, 1));
          UNITY_TRANSFER_FOG(o,o.vertex);
          TriStream.Append(o);
          TriStream.RestartStrip();

          o.vertex = UnityObjectToClipPos(float4(v1.vertex.xyz, 1));
          UNITY_TRANSFER_FOG(o,o.vertex);
          TriStream.Append(o);

          o.vertex = UnityObjectToClipPos(float4(v1.vertex.xyz + dir * _Width, 1));
          UNITY_TRANSFER_FOG(o,o.vertex);
          TriStream.Append(o);

          o.vertex = UnityObjectToClipPos(float4(v2.vertex.xyz + dir * _Width, 1));
          UNITY_TRANSFER_FOG(o,o.vertex);
          TriStream.Append(o);
          TriStream.RestartStrip();
        }

      }

      fixed4 frag (g2f i) : SV_Target {
        fixed4 col = i.color;
        UNITY_APPLY_FOG(i.fogCoord, col);
        return col;
      }
      ENDCG
    }
    

  }
}