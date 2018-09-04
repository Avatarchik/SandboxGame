// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//one directional light

Shader "PengLu/Toon/ToonRampMapVF" {
	Properties{
		_Color("Main Color",color) = (1,1,1,1)
		_OutlineColor("Outline Color",color) = (0.1,0.1,0.2,1)
		_MainTex("BaseTex", 2D) = "white" {}
	_ToonMap("Ramp Map",2D) = "white"{}
	_Outline("Thick of Outline",range(0,0.1)) = 0.02
		_Factor("Factor",range(0,1)) = 0.5
		_ToonEffect("Toon Effect",range(0,1)) = 0.5
	}
		SubShader{
		pass {
		Tags{ "LightMode" = "Always" }
			Cull Front
			ZWrite On
			CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
			float _Outline;
		float _Factor;
		float4 _OutlineColor;

		struct v2f {
			float4 pos:SV_POSITION;
		};

		v2f vert(appdata_full v) {
			v2f o;
			float3 dir = normalize(v.vertex.xyz);
			float3 dir2 = v.normal;
			float D = dot(dir,dir2);
			dir = dir * sign(D);
			dir = dir * _Factor + dir2 * (1 - _Factor);
			v.vertex.xyz += dir * _Outline;
			o.pos = UnityObjectToClipPos(v.vertex);
			return o;
		}
		float4 frag(v2f i) :COLOR
		{
			float4 c = _OutlineColor;
			return c;
		}
			ENDCG
	}//end of pass

	pass {
		Tags{ "LightMode" = "ForwardBase" }
			Cull Back
			CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

			sampler2D _ToonMap;
		sampler2D _MainTex;
		float4 _MainTex_ST;
		float4 _LightColor0;
		float4 _Color;
		float _ToonEffect;

		struct v2f {
			float4 pos:SV_POSITION;
			float3 lightDir:TEXCOORD0;
			float3 normal:TEXCOORD1;
			float2 texcoord:TEXCOORD2;
		};

		v2f vert(appdata_full v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.normal = v.normal;
			o.lightDir = ObjSpaceLightDir(v.vertex);
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			return o;
		}
		float4 frag(v2f i) :COLOR
		{
			float4 c = 1;
			float3 N = normalize(i.normal);
			float3 lightDir = normalize(i.lightDir);
			float diff = max(0,dot(N,lightDir));
			diff = (diff + 1) / 2;
			diff = smoothstep(0,1,diff);
			float toon = tex2D(_ToonMap,float2(diff,0.5)).r;
			diff = lerp(diff,toon,_ToonEffect);
			c = _Color * _LightColor0*(diff);
			c *= tex2D(_MainTex,i.texcoord);
			return c;
		}
			ENDCG
	}
	}
}

//// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
//
//Shader "Unlit/NewUnlitShader"
//{
//	Properties
//	{
//		_MainTex ("Texture", 2D) = "white" {}
//	}
//	SubShader
//	{
//		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" }
//		LOD 100
//		Pass
//	{
//		Name "OUTLINE"
//		Cull Front
//		CGPROGRAM
//
//#pragma vertex vert  
//#pragma fragment frag  
//
//#include "UnityCG.cginc"  
//
//		float _Outline;
//
//	fixed4 _OutlineColor;
//
//	struct a2v {
//		float4 vertex : POSITION;
//		float3 normal : NORMAL;
//		float4 texcoord : TEXCOORD0;
//		float4 tangent : TANGENT;
//	};
//
//	struct v2f {
//		float4 pos : SV_POSITION;
//		float2 uv : TEXCOORD0;
//		float3 worldViewDir  : TEXCOORD1;
//		float3 worldNormal : TEXCOORD2;
//	};
//
//	v2f vert(a2v v)
//	{
//		v2f o;
//
//		float4 pos = mul(UNITY_MATRIX_MV, v.vertex);//顶点变换到视角空间 View Space  
//		float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);//法线变换到视角空间  
//		normal.z = -0.5;
//		float4 newNormal = float4(normalize(normal), 0); //归一化以后的normal  
//		pos = pos + newNormal * _Outline; //沿法线方向扩大_Outline个距离  
//		o.pos = mul(UNITY_MATRIX_P, pos);
//
//		float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
//		o.worldViewDir = _WorldSpaceCameraPos.xyz - worldPos;//得到世界空间的视线方向  
//		o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
//
//		return o;
//	}
//
//	float4 frag(v2f i) : SV_Target
//	{
//		return float4(_OutlineColor.rgb, 1);;
//	}
//
//		ENDCG
//	}
//
//		Pass{
//		Tags{ "LightMode" = "ForwardBase" }
//
//		Cull off
//
//		CGPROGRAM
//
//#pragma vertex vert  
//#pragma fragment frag  
//
//#pragma multi_compile_fwdbase  
//
//#include "UnityCG.cginc"  
//#include "Lighting.cginc"  
//#include "AutoLight.cginc"  
//#include "UnityShaderVariables.cginc"  
//
//		fixed4 _Color;
//	sampler2D _MainTex;
//	float4 _MainTex_ST;
//	sampler2D _Bump;
//	float4 _Bump_ST;
//	sampler2D _Ramp;
//	float _DetailOutLineSize;
//	fixed4 _DetailOutLineColor;
//	fixed4 _Specular;
//	fixed _SpecularScale;
//	fixed _MainHairSpecularSmooth;
//	fixed _FuHairSpecularSmooth;
//	float _MainHairSpecularOff;
//	float _FuHairSpecularOff;
//	sampler2D _HairLightRamp;
//	float4 _HairLightRamp_ST;
//	float _RefractionCount;
//	float _ReflectionCount;
//	float _edgeLightOff;
//	sampler2D _LightMapMask;
//
//	struct a2v {
//		float4 vertex : POSITION;
//		float3 normal : NORMAL;
//		float4 texcoord : TEXCOORD0;
//		float4 tangent : TANGENT;
//	};
//
//	struct v2f {
//		float4 pos : POSITION;
//		float2 uv : TEXCOORD0;
//		float3 worldNormal : TEXCOORD1;
//		float3 worldPos : TEXCOORD2;
//
//		SHADOW_COORDS(3)
//			float3 tangent : TEXCOORD4;
//		float2 hairLightUV:TEXCOORD5;
//		float2 uv_Bump : TEXCOORD6;
//		float3 normal : TEXCOORD7;
//	};
//
//	v2f vert(a2v v) {
//		v2f o;
//
//		o.pos = UnityObjectToClipPos(v.vertex);
//		o.normal = v.normal;
//		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
//		o.hairLightUV = TRANSFORM_TEX(v.texcoord, _HairLightRamp);
//		o.uv_Bump = TRANSFORM_TEX(v.texcoord, _Bump);
//		o.worldNormal = UnityObjectToWorldNormal(v.normal);
//		o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
//		TRANSFER_SHADOW(o);
//		//求出沿着发梢到发根方向的切线  
//		half4 p_tangent = mul(unity_ObjectToWorld, v.tangent);
//		o.tangent = normalize(p_tangent).xyz;
//		o.tangent = cross(o.tangent, o.worldNormal);
//		return o;
//	}
//
//	float HairSpecular(fixed3 halfDir, float3 tangent, float specularSmooth)
//	{
//		float dotTH = dot(tangent, halfDir);
//		float sqrTH = max(0.01,sqrt(1 - pow(dotTH, 2)));
//		float atten = smoothstep(-1,0, dotTH);
//
//		//头发主高光值  
//		float specMain = atten * pow(sqrTH, specularSmooth);
//		return specMain;
//	}
//
//	float3 LightMapColor(fixed3 worldLightDir,fixed3 worldNormalDir,fixed2 uv)
//	{
//		float LdotN = max(0, dot(worldLightDir, worldNormalDir));
//		float3 lightColor = LdotN * tex2D(_LightMapMask, uv);
//		return lightColor;
//	}
//
//	float4 frag(v2f i) : SV_Target{
//		fixed3 worldNormal = normalize(i.worldNormal);
//	fixed3 tangentNormal = UnpackNormal(tex2D(_Bump, i.uv_Bump));
//	fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
//	fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
//	fixed3 worldHalfDir = normalize(worldLightDir + worldViewDir);
//
//	//漫反射贴图采样  
//	fixed4 c = tex2D(_MainTex, i.uv);
//	fixed3 albedo = c.rgb * _Color.rgb;
//
//	fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
//
//	UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);//阴影值计算  
//
//	fixed diff = dot(worldNormal, worldLightDir); //世界空间的法线坐标和光照方向点乘得到漫反射颜色  
//	diff = (diff * 0.5 + 0.5) * atten; //暗部提亮  当然这里也可以不提亮  
//
//									   //将光线颜色和环境光颜色以及梯度图采样相乘得到最终的漫反射颜色  
//	fixed3 diffuse = _LightColor0.rgb * albedo * tex2D(_Ramp, float2(diff, diff)).rgb;
//
//	//头发高光图采样  
//	float3 speTex = tex2D(_HairLightRamp, i.hairLightUV);
//	//头发主高光偏移                 
//	float3 Ts = i.tangent + worldNormal * _MainHairSpecularOff * speTex;
//	//头发副高光偏移  
//	float3 Tf = i.tangent + worldNormal * _FuHairSpecularOff * speTex;
//
//	//头发主高光值  
//	float specMain = HairSpecular(worldHalfDir,Ts, _MainHairSpecularSmooth);
//	float specFu = HairSpecular(worldHalfDir,Tf, _FuHairSpecularSmooth);
//
//	float specFinal = specMain;
//
//	fixed3 specular = _Specular.rgb * specFinal * atten;
//
//	half edge = abs(dot(worldNormal, worldViewDir)); //计算边缘光  
//	float Fr = pow(1 - edge, _ReflectionCount)* atten;//反射值  
//	float Ft = pow(edge, _RefractionCount)* atten;//折射值  
//
//	fixed3 lightMapColor = LightMapColor(worldLightDir, worldNormal,i.uv).rgb;
//
//	//计算法线勾边  
//	//half normalEdge = saturate(dot(i.normal, worldViewDir));  
//	//normalEdge = normalEdge < _DetailOutLineSize ? normalEdge / 4 : 1;  
//
//	return fixed4(ambient + diffuse + specular, 1.0) + Fr;
//	}
//
//		ENDCG
//	}
//		
//
//
//	}
//	Fallback "Diffuse"
//}
