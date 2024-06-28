Shader "Custom/WireframeShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _WireColor ("Wireframe Color", Color) = (0,0,0,1)
        _WireThickness ("Wireframe Thickness", Range (0.001, 0.1)) = 0.01
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
            };

            float _WireThickness;
            fixed4 _Color;
            fixed4 _WireColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = _WireColor;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
