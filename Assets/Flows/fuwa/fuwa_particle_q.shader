// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/fuwa_particle_q"
{
	Properties
	{
		_Height ("Height", Float) = 0.05
		_Size ("Particle Size", Float) = 1.0
		_Fuwa ("Fuwa Rate", Range(0,1)) = 0.3
		_FuwaDuration ("Fuwa Duration", Float) = 1.0
		_FuwaTarget ("Fuwa Target", Vector) = (0,1,0,0)
		_Speed ("Randomize Speed", Float) = 1.0
		_ScatterFactor ("Scatter Factor", Float) = 8
		_ScatterDistance ("Scatter Distance", Float) = 1
		_Color ("Color", Color) = (0.3,0.6,1.0,1.0)
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "DisableBatching"="True" }
		LOD 100
		Cull Off
		Blend SrcAlpha One
		ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 color : TEXCOORD1;
				float d : TEXCOORD2;
			};

			sampler2D _MainTex;
			float4 _Color;
			float _Height;
			float _Fuwa;
			float _FuwaDuration;
			float4 _FuwaTarget;
			float _Size;
			float _Speed;
			float _ScatterFactor;
			float _ScatterDistance;

			float rand(float2 co){
				return frac(sin(dot(co.xy, float2(12.9898,78.233))) * 43758.5453);
			}
			
			float stepping(float t){
				if(t<0.)return -1.+pow(1.+t,2.);
				else return 1.-pow(1.-t,2.);
			}
			v2f vert (appdata i)
			{
				float2 uv = i.vertex.xy * 0.5 * float2(-1,1) + 0.5; // [0,1]
				uv.x = (floor(uv.x * 256) + 0.5) / 256.0;
				uv.y = (floor(uv.y * 128) + 0.5) / 128.0;
				v2f o;
				o.color = float3(1,1,1);
				float size = 1.0;
				
				float r0 = rand(uv+0), r1 = rand(uv+1), r2 = rand(uv+2), r3 = rand(uv+3), r4 = rand(uv+4);
				float3 p = 0, v = 0;
			
				float t = _Time.y * _Speed;
				float fth = r0*3.1415926535*2;
				float fu = r1*2-1;
				float3 fpos = float3(sqrt(1-fu*fu)*float2(cos(fth),sin(fth)),0).xzy + float3(0,fu,0);
				float3 xpos = fpos;
				float fr = t / 2.0 + r0 * 500;
				fpos.xy = mul(fpos.xy, float2x2(cos(fr),-sin(fr),sin(fr),cos(fr)));
				fr = t / 3.0 + r1 * 500;
				fpos.yz = mul(fpos.yz, float2x2(cos(fr),-sin(fr),sin(fr),cos(fr)));
				
				if(true) {
					float th = r0 * 3.1415926535 * 2.0 + r2;
					float r = sqrt(r1 + 0.0008) * (1 + exp(-r2*_ScatterFactor) * _ScatterDistance);
					p = float3(cos(th), 0, sin(th)) * r * 2.0;
					p += fpos * _Height;
					p.y += _Height;
					o.color = _Color;
					if(r3 < _Fuwa) {
						float m = (exp(-r4*10) + 1.0) * 3.0;
						float hv = fmod((100+_Time.y)*(r3*1.0+1.0)*0.5/_FuwaDuration, m);
						p += hv * 0.1 * _FuwaTarget.xyz;
						o.color *= pow(sin(hv/m*3.1415926535),0.5);
					}
					float thv = r3 * 3.1415926535 * 2.0;
					o.color *= 0.5;
					o.color *= max(0, min(1, 1 + r3));
					size = 2.0 + sin(r2*3.1415926535*2.0 + _Time.y*0.3*(1+r2));
					size *= _Size;
				}

				o.d = 0;

				size *= 2;
				float sz = 0.002 * size;
				float4 viewOrig = mul(UNITY_MATRIX_MV, float4(p,1));
				
				float2 ouv = float2((floor(uv.x*128) + 0.5) / 128.0, uv.y);
				if(i.uv.x < ouv.x && i.uv.y > ouv.y) {
					o.uv = float2(-0.9,-0.5) * 2;
				} else if(i.uv.x > ouv.x && i.uv.y < ouv.y) {
					o.uv = float2(0.9,-0.5) * 2;
				} else {
					o.uv = float2(0,1) * 2;
				}
				o.vertex = mul(UNITY_MATRIX_P, viewOrig+float4(o.uv*sz,0,0));
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float l = length(i.uv);
				clip(1-l);
				float3 color = i.color;
				color *= pow(max(0, 0.5 - i.d) + 1 - l, 0.5) * 2;
				color = min(1, color);
				color = pow(color, 2.2);
				return float4(color,smoothstep(1,0.8,l)*_Color.a);
			}
			ENDCG
		}
	}
}
