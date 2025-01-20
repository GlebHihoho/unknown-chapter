// Шейдер для светящегося маркера с отсечением верхней части
Shader "Custom/GlowMarkerWithDiscard" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _GlowColor ("Glow Color", Color) = (1, 1, 1, 1)
        _GlowIntensity ("Glow Intensity", Range(0, 1)) = 1
        _BottomThreshold ("Bottom Threshold", Range(0, 1)) = 0.2
    }
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _GlowColor;
            float _GlowIntensity;
            float _BottomThreshold;
            
            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            half4 frag (v2f i) : SV_Target {
                half4 col = tex2D(_MainTex, i.uv) * _Color;
                half4 glow = lerp(col, _GlowColor, _GlowIntensity);
                
                // Проверяем, находится ли текущий пиксель ниже порога
                if (i.uv.y < _BottomThreshold) {
                    // Пиксель подсвечен, оставляем его видимым
                    return glow;
                } else {
                    // Пиксель выше порога, отсекаем его
                    discard;
                    return half4(0, 0, 0, 0); // Эта строка не будет достигнута, но добавлена для корректности
                }
            }
            ENDCG
        }
    }
}
