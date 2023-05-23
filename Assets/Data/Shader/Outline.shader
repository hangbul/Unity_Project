Shader "Unlit/Outline"
{
    Properties
    {
        _Color("OutLine Color", Color) = (1,0,0,1)
        _Thickness("Line Thickness", Range(0.0, 1.0)) = 0.01
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 101

        Pass
        {
            Zwrite off
            Ztest Always

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float4 _Color;
            float4 _Thickness;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
                float2 offset = TransformViewToProjection(normal.xy);
                o.pos.xy += offset * _Thickness;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
