Shader "Shader Jam/ice" {
	Properties {
		_Color ("Color", Color) = (0.5, 0.5, 0.5, 0.5)
      _SpecColor ("Specular Material Color", Color) = (1,1,1,1) 
      _Shininess ("Shininess", Float) = 10
	}
	SubShader {
		Tags { "Queue"="Transparent"
			"LightMode"="ForwardBase" }
		LOD 200
		
		Pass {  
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			
			#include "UnityCG.cginc"
			
         	uniform float4 _LightColor0; 
			uniform float4 _Color;
			
			uniform float4 _SpecColor; 
			uniform float _Shininess;

			struct vertexInput {
				float4 vertex : POSITION;
            	float3 normal : NORMAL;
			};

			struct vertexOutput {
				float4 vertex : SV_POSITION;
            	float4 color : COLOR0;
			};
			
			vertexOutput vert(vertexInput input) 
			{
				vertexOutput output;
				
				float3 normalDirection = normalize(mul(float4(input.normal, 0.0), _World2Object).xyz);
            	float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				
				// diffuse
            	float3 diffuseReflection = _LightColor0.rgb * _Color.rgb * max(0.0, dot(normalDirection, lightDirection));
            	float3 viewDirection = normalize(_WorldSpaceCameraPos - mul(_Object2World, input.vertex).xyz);
 
 				// ambient light
 				float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - mul(modelMatrix, input.vertex * _WorldSpaceLightPos0.w).xyz;
            	float one_over_distance =  1.0 / length(vertexToLightSource);
            	float attenuation = lerp(1.0, one_over_distance, _WorldSpaceLightPos0.w); 
            	float3 lightDirection = vertexToLightSource * one_over_distance;
            	
				float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;
            	
 
 				// specular light
	            float3 specularReflection;
	            
	            if (dot(normalDirection, lightDirection) < 0.0) 
	               // light source on the wrong side?
	            {
					specularReflection = float3(0.0, 0.0, 0.0);
	            }
	            else // light source on the right side
	            {
	            	
	               specularReflection = attenuation * _LightColor0.rgb 
	                  * _SpecColor.rgb * pow(max(0.0, dot(
	                  reflect(-lightDirection, normalDirection), 
	                  viewDirection)), _Shininess);
	            }
				
				output.vertex = mul(UNITY_MATRIX_MVP, input.vertex);
				output.color = float4(diffuseReflection + specularReflection + ambientLighting, 1.0);
				return output;
			}
			
			fixed4 frag (vertexOutput input) : COLOR
			{
				fixed4 col = input.color;
				
				return col;
			}
		ENDCG
		}
	}
}
