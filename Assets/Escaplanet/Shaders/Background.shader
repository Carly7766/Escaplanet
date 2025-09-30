Shader "Escaplanet/Unlit/Background"
{
    Properties
    {
        [HideInInspector]_MainTex ("Diffuse", 2D) = "white" {}
        _WorldScale ("World Scale", Float) = 3
        _WorldOffset ("World Offset", Vector) = (0 , 0, 0, 0)
        _CellOffset ("Cell Offset", Vector) = (137.2, 319.8, 0, 0)
        _Density ("Density", Range(0, 2)) = 0.02
        _Intensity ("Intensity", Range(0, 1)) = 0.08
        _SkyColor ("Sky Color", Color) = (0.03, 0.05, 0.11)
        [HDR] _StarColor ("Star Color", Color) = (0.8, 1.0, 0.3)
        _StarHueOffset("Star Hue Offset", Range(0,1)) = 0.6
    }

    HLSLINCLUDE
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    ENDHLSL

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            CBUFFER_START(UnityPerMaterial)
                uniform float _WorldScale;
                uniform float2 _WorldOffset;
                uniform float2 _CellOffset;
                uniform float _Density;
                uniform float _Intensity;
                uniform float3 _SkyColor;
                uniform float3 _StarColor;
                uniform float _StarHueOffset;
            CBUFFER_END

            struct Attributes
            {
                float3 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD1;
            };

            float random(float seed)
            {
                return frac(sin(seed) * 43758.5453123);
            }

            float2 random2(float2 seed)
            {
                float u = dot(seed, float2(127.1, 311.7));
                float v = dot(seed, float2(269.5, 183.3));
                return frac(sin(float2(u, v)) * 43758.5453);
            }

            struct VoronoiOutput
            {
                float m_dist;
                float m_dist2;
                float2 m_point;
            };

            VoronoiOutput voronoi(float2 uv)
            {
                float2 i_uv = floor(uv);
                float2 f_uv = frac(uv);

                float m_dist = 1e10;
                float m_dist2 = 1e10;
                float2 m_point = 0.0;

                for (int y = -1; y <= 1; y++)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        float2 neighbor = float2(x, y);
                        float2 cell = i_uv + neighbor;

                        if (random2(cell).x < _Density)
                        {
                            float2 feat = random2(cell + _CellOffset);

                            float2 diff = neighbor + feat - f_uv;

                            float dist2 = dot(diff, diff);

                            if (dist2 < m_dist2)
                            {
                                m_dist2 = dist2;
                                m_dist = sqrt(dist2);
                                m_point = feat;
                            }
                        }
                    }
                }
                VoronoiOutput output = (VoronoiOutput)0;

                output.m_dist = m_dist;
                output.m_dist2 = m_dist2;
                output.m_point = m_point;

                return output;
            }

            float3 hue(float3 input, float offset)
            {
                float4 k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 p = lerp(float4(input.bg, k.wz), float4(input.gb, k.xy), step(input.b, input.g));
                float4 q = lerp(float4(p.xyw, input.r), float4(input.r, p.yzx), step(p.x, input.r));
                float d = q.x - min(q.w, q.y);
                float e = 1.e-10;
                float3 hsv = float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);

                float hue = hsv.x + offset;
                hsv.x = (hue < 0) ? hue + 1 : (hue > 1) ? hue - 1 : hue;

                float4 k2 = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 p2 = abs(frac(hsv.xxx + k2.xyz) * 6.0 - k2.www);
                float3 rgb = hsv.z * lerp(k2.xxx, saturate(p2 - k2.xxx), hsv.y);

                return rgb;
            }

            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;
                output.positionCS = TransformObjectToHClip(input.positionOS);
                output.positionWS = TransformObjectToWorld(input.positionOS);
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float2 worldUV = input.positionWS.xy * _WorldScale + _WorldOffset.xy;
                VoronoiOutput v = voronoi(worldUV);

                // float d = smoothstep(_Intensity, 0, v.m_dist);
                float d = exp(-v.m_dist * _Intensity * 100.0);

                float layer = v.m_point.x;
                float rand = random(v.m_point.x);

                float3 skyColor = _SkyColor.rgb;

                float3 starColor = d * hue(skyColor + _StarColor, rand * _StarHueOffset);

                float3 col = skyColor + starColor;

                return half4(col, 1);
            }
            ENDHLSL
        }
    }
}