
Shader "Shader Jam/distanceShader" {
Properties {
	_OtherCirclePos ("Other Circle Position", Vector) = (0, 0, 0, 0)
	_Color ("Color", Color) = (1, 0, 0, 1)
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 100
	
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			
			#include "UnityCG.cginc"
			
			uniform float4 _OtherCirclePos;
			uniform float4 _Color;

			struct vertexInput {
				float4 vertex : POSITION;
            	float3 normal : NORMAL;
			};

			struct vertexOutput {
				float4 vertex : SV_POSITION;
            	float4 direction_distance : COLOR0;
			};
			
			vertexOutput vert(vertexInput input) 
			{
				vertexOutput output;
				
				float4 worldPos = mul(_Object2World, input.vertex);
				float4 dist = _OtherCirclePos - worldPos;
				float mag = sqrt(dist.x * dist.x + dist.y * dist.y + dist.z * dist.z);
				dist = normalize(dist) * pow(mag * 1, 5); 
				
				float4 pos = input.vertex + dist;
				pos.w = 1;
				
				output.vertex = mul(UNITY_MATRIX_MVP, pos);
		 		
				output.direction_distance = float4(dist.x, dist.y, dist.z, 1);
				
				return output;
			}
			
			fixed4 frag (vertexOutput input) : COLOR
			{
				fixed4 col = input.direction_distance;
				
				return col;
			}
		ENDCG
	}
	//Fallback "VertexLit"
}

}
