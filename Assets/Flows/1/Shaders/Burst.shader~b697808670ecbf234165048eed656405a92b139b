Shader "Custom/VJ/Burst"
{
  Properties 
  {
    _Face("Face", Float) = 0
    _MainTex("MainTex", 2D) = "white"{}
    _Emission("Emission", Color) = (1,1,1,1)
    _ScaleBox("ScaleBox", Vector) = (1,1,1,1)
    _Progress("Progress", Range(0,1)) = 1
    _Scatter("Scatter", Float) = 1
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
        float3 uv : TEXCOORD2;
      };

      float _Face;
      sampler2D _MainTex;
      fixed4 _Emission;
      fixed3 _ScaleBox;
      fixed _Progress;
      fixed _Scatter;
      


      v2g vert (appdata v)
      {
        v2g o;
        o.vertex = v.vertex;
        o.uv = v.uv.xy;
        o.normal = v.normal;
        return o;
      }

      float rand(float3 co){
			  return frac(sin(dot(co.xyz, float3(12.9898,78.233,82.195))) * 43758.5453);
		  }

      float4x4 eulerAnglesToRotationMatrix(float3 angles)
		  {
			  float ch = cos(angles.y); float sh = sin(angles.y); // heading
			  float ca = cos(angles.z); float sa = sin(angles.z); // attitude
			  float cb = cos(angles.x); float sb = sin(angles.x); // bank

			  // Ry-Rx-Rz (Yaw Pitch Roll)
			  return float4x4(
			  	ch * ca + sh * sb * sa, -ch * sa + sh * sb * ca, sh * cb, 0,
			  	cb * sa, cb * ca, -sb, 0,
			  	-sh * ca + ch * sb * sa, sh * sa + ch * sb * ca, ch * cb, 0,
			  	0, 0, 0, 1
			  );
		  }

      float CubicBezier(float2 p1,float2 p2, float t)
      {
        float2 s = float2(0,0);
        float2 e = float2(1,1);
        return (pow(1-t,3) * s + 3 * pow(1-t,2) * t * p1 + 3 * (1-t) * t * t * p2 + t * t * t * e).y;
      }

      float easeInExpo(float t)
      {
        return CubicBezier(float2(0.95,0.05),float2(0.795,0.035),t);
      }


      float3 CalNorm(float3 v1,float3 v2, float3 v3)
      {
        return normalize(cross(v3-v2,v1-v2));
      }

      float Normal(float v)
      {
        return min(max(0,v),1);
      }

      [maxvertexcount(22)]
      void geom(triangle v2g v[3], inout TriangleStream<g2f> stream)
      {
        g2f o;
        o.color = float4(1.0,1.0,1.0,1.0);
        float4 v1 = (v[0].vertex);
        float4 v2 =(v[1].vertex);
        float4 v3 = (v[2].vertex);
        float4 center = (v1 + v2 + v3) / 3;
        float prog =  easeInExpo(Normal( 1 - abs(sin( _Time.y / 10. + center.y*_Progress))));
        float3 rad = float3((rand(v1)*2-1)*6.283,(rand(v2)*2-1)*6.283,(rand(v3)*2-1)*6.283);
        float4 pos = float4(CalNorm(v1.xyz,v2.xyz,v3.xyz),1)*prog*_Scatter;// + float4((rand(v1+rad)*2-1)*_Scatter*_ScaleBox.x*prog,(rand(v2+rad)*2-1)*_Scatter*_ScaleBox.y*prog,(rand(v3+rad)*2-1)*_Scatter*_ScaleBox.z*prog,1);
        float4x4 mat = eulerAnglesToRotationMatrix(rad * prog);
        float4 rv1 = mul(mat, v1 - center) + pos + center;
        float4 rv2 = mul(mat, v2 - center) + pos + center;
        float4 rv3 = mul(mat, v3 - center) + pos + center;
        o.normal = CalNorm(rv1,rv2,rv3);

        o.vertex = UnityObjectToClipPos(rv1);
        o.uv = float3(v[0].uv,1);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(rv2);
        o.uv = float3(v[1].uv,1);
        stream.Append(o);
        o.vertex = UnityObjectToClipPos(rv3);
        o.uv = float3(v[2].uv,1);
        stream.Append(o);
        stream.RestartStrip();

      }

      fixed4 frag(g2f i) : SV_TARGET
      {
        half nl = max(0, dot(i.normal, _WorldSpaceLightPos0.xyz));
        fixed4 col;
        col = fixed4(tex2D(_MainTex, i.uv).xyz * nl,1)+_Emission;
        return col;
      }
      ENDCG
    }
  }
}
