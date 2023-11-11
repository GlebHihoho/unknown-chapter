Shader "Custom/RaycastMaskShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskTexture ("Mask Texture", 2D) = "white" {}
        _MaskPosition ("Mask Position", Vector) = (0, 0, 0, 0)
        _MaskRadius ("Mask Radius", Range(0, 1)) = 0.1
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Opaque" }
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            sampler2D _MaskTexture;
            float4 _MainTex_ST;
            float4 _MaskPosition;
            float _MaskRadius;
 
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }
 
            half4 frag (v2f i) : SV_Target
            {
                // Определение глубины пикселя из текстуры глубины
                float depth = tex2D(_MainTex, i.texcoord).a;
 
                // Определение порога глубины, при котором текстура становится прозрачной
                float depthThreshold = 0.2; // Настройте подходящее значение
 
                // Определение расстояния от текущей позиции до маскировочной точки
                float2 distanceToMask = i.texcoord - _MaskPosition.xy;
 
                // Вычисление длины вектора расстояния
                float distance = length(distanceToMask);
 
                // Если глубина текущего пикселя меньше порога и расстояние до маскировочной точки меньше радиуса,
                // делаем его частично прозрачным на основе текстуры маски
                if (depth < depthThreshold && distance <= _MaskRadius)
                {
                    half4 maskColor = tex2D(_MaskTexture, i.texcoord);
                    return half4(maskColor.rgb, maskColor.a * depth); // Прозрачность основана на альфа-канале текстуры маски
                }
                else
                {
                    // В противном случае, используем основную текстуру
                    return tex2D(_MainTex, i.texcoord);
                }
            }
            ENDCG
        }
    }
}
