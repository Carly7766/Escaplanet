Shader "Escaplanet/Lit/Planet"
{
    Properties
    {
        _MainTex("Diffuse", 2D) = "white" {}

        _Center("Center", Vector) = (0.5, 0.5, 0, 0)
        _Radius("Radius", Float) = 0.5
        _Strength("Strength", Range(-1, 1)) = 1

        _EdgeSoftness("Edge Softness", Range(0, 0.1)) = 0.01

        _ColorA ("Band Color A", Color) = (0.98,0.75,0.35,1)
        _ColorB ("Band Color B", Color) = (1.0,0.88,0.62,1)

        _BandFrequency("Band Frequency", Float) = 12.0
        _BandSoftness("Band Softness", Range(0.0, 0.5)) = 0.15

        _Frequency("Frequency", Float) = 1.0
        _Lacunarity("Lacunarity", Float) = 2.0
        _Amplitude("Amplitude", Float) = 0.5
        _Gain("Gain", Float) = 0.5
        _Octaves("Noise Octaves", Int) = 10

        _Seed("Seed", Float) = 0.0
        _Rotation("Rotation (deg)", Range(-180, 180)) = 20.0
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
            "RenderPipeline"= "UniversalPipeline"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag

            struct Attributes
            {
                float3 positionOS : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                half4 color : COLOR;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float2 _Center;
                float _Radius;
                float _Strength;

                float _EdgeSoftness;

                float4 _ColorA;
                float4 _ColorB;

                float _BandFrequency;
                float _BandSoftness;

                float _Frequency;
                float _Lacunarity;
                float _Amplitude;
                float _Gain;
                int _Octaves;

                float _Seed;
                float _Rotation;
            CBUFFER_END

            Varyings vert(Attributes attributes)
            {
                Varyings o = (Varyings)0;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.positionCS = TransformObjectToHClip(attributes.positionOS);
                // o.uv = TRANSFORM_TEX(attributes.uv, _MainTex);
                o.uv = attributes.uv;
                o.color = attributes.color;
                return o;
            }

            float2 SphericalWarpUV(float2 uv, float2 center, float radius, float strength)
            {
                float2 d = uv - center;
                float dist = length(d);
                float r = dist / max(radius, REAL_EPS);
                float t = saturate(abs(strength));
                float sgn = step(0.0, strength);

                float target = lerp(sin(r * HALF_PI), asin(saturate(r)) * INV_HALF_PI, sgn);

                float rWarped = mad(t, (target - r), r);

                float inRange = step(REAL_EPS, r) * step(r, 1.0);
                float scale = lerp(1.0, rWarped / max(r, REAL_EPS), inRange);

                return center + d * scale;
            }

            float random2(in float2 p)
            {
                return frac(sin(dot(p, float2(12.9898, 78.233))) * 43758.5453);
            }

            float noise(float2 p)
            {
                float2 i = floor(p);
                float2 f = frac(p);

                float a = random2(i + float2(0.0, 0.0));
                float b = random2(i + float2(1.0, 0.0));
                float c = random2(i + float2(0.0, 1.0));
                float d = random2(i + float2(1.0, 1.0));
                float2 u = f * f * (3.0 - 2.0 * f);

                return lerp(lerp(a, b, u.x), lerp(c, d, u.x), u.y);
            }

            float fbm(in float2 st, float frequency, float lacunarity, float amplitude, float gain, int octaves)
            {
                float value = 0.0;

                for (int i = 0; i < octaves; i++)
                {
                    value += amplitude * noise(frequency * st);
                    frequency *= lacunarity;
                    amplitude *= gain;
                }
                return value;
            }

            half4 a()
            {
            }

            half4 frag(Varyings i) : SV_Target
            {
                // half4 col = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvWarped);
                float2 uvWarped = SphericalWarpUV(i.uv, _Center, _Radius, _Strength);
                float mask = smoothstep(_Radius, _Radius - _EdgeSoftness, distance(i.uv, _Center));

                float r = radians(_Rotation);
                float cs = cos(r), sn = sin(r);
                float2 p = (uvWarped - _Center) * 2.0;
                float2 pr = float2(cs * p.x - sn * p.y, sn * p.x + cs * p.y);

                float n = fbm(pr * _Frequency + float2(_Seed, 0), _Frequency, _Lacunarity, _Amplitude, _Gain, _Octaves);
                n = (n - 0.5) * 2.0;

                float bands = 0.5 + 0.5 * sin((pr.y + n) * _BandFrequency * 6.2831853);
                float k = smoothstep(0.5 - _BandSoftness, 0.5 + _BandSoftness, bands);

                half4 col = lerp(_ColorA, _ColorB, k);

                col.rgb *= i.color.rgb;
                col.a *= i.color.a;

                col.a *= mask;
                col.rgb *= col.a;
                return half4(col);
            }
            ENDHLSL
        }
    }

    Fallback "Sprites/Default"
}