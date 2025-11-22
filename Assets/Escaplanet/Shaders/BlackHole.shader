Shader "Custom/SpriteSpiralStripesURP"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

        _ColorA ("Stripe Color A", Color) = (1,1,1,1)
        _ColorB ("Stripe Color B", Color) = (0,0,0,1)

        _Center ("Center (UV)", Vector) = (0.5, 0.5, 0, 0)
        _Scale ("Scale XY", Vector) = (1, 1, 0, 0)

        _StripePairs ("Stripe Pairs", Range(1, 32)) = 10
        _Twist ("Twist", Range(-20, 20)) = 10
        _EdgeSoftness ("Edge Softness", Range(0.0, 0.3)) = 0.02

        _RotateSpeed ("Rotate Speed", Float) = 2.0

        // 外側の円形マスク
        _CircleRadius ("Circle Radius (UV)", Range(0.0, 1.0)) = 0.5
        _CircleEdgeSoftness ("Circle Edge Softness", Range(0.0, 0.5)) = 0.02

        // ★ 追加：中心の穴用
        _HoleRadius ("Hole Radius (UV)", Range(0.0, 1.0)) = 0.15
        _HoleEdgeSoftness ("Hole Edge Softness", Range(0.0, 0.5)) = 0.02
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

            struct Attributes
            {
                float3 positionOS : POSITION;
                float2 uv: TEXCOORD0;
                float4 color: COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS: SV_POSITION;
                float2 uv: TEXCOORD0;
                float4 color: COLOR;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            CBUFFER_START(UnityPerMaterial)
                float4 _ColorA;
                float4 _ColorB;
                float4 _Center;
                float4 _Scale;
                float _StripePairs;
                float _Twist;
                float _EdgeSoftness;
                float _RotateSpeed;

                float _CircleRadius;
                float _CircleEdgeSoftness;

                // ★ 追加：中心の穴
                float _HoleRadius;
                float _HoleEdgeSoftness;
            CBUFFER_END

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            Varyings vert(Attributes attributes)
            {
                Varyings o = (Varyings)0;
                UNITY_SETUP_INSTANCE_ID(attributes);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.positionCS = TransformObjectToHClip(attributes.positionOS);
                o.uv = attributes.uv;
                o.color = attributes.color;
                return o;
            }

            half4 frag(Varyings i): SV_Target
            {
                // スパイラル模様用：スケール付き座標
                float2 p = (i.uv - _Center.xy) * _Scale.xy;

                float angle = atan2(p.y, p.x);
                float len = length(p);

                float spiralAngle = angle + len * _Twist;
                float wave = sin(spiralAngle * _StripePairs - _Time.y * _RotateSpeed);

                float t = smoothstep(-_EdgeSoftness, _EdgeSoftness, wave);

                float4 col = lerp(_ColorB, _ColorA, t);
                col *= i.color;

                // ベーススプライトのアルファ
                float alphaMask = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv).a;

                // 円形マスク用：元のUV空間で距離を計算
                float2 q = i.uv - _Center.xy;
                float dist = length(q);

                // 外側の円形マスク（外側をフェードアウト）
                float outerMask = 1.0 - smoothstep(_CircleRadius,
                                                   _CircleRadius + _CircleEdgeSoftness,
                                                   dist);

                // ★ 中心の穴マスク
                //  dist が _HoleRadius より小さいところを 0（透明）に、外側を 1（不透明）にする
                float innerMask = smoothstep(_HoleRadius - _HoleEdgeSoftness,
                                             _HoleRadius + _HoleEdgeSoftness,
                                             dist);

                // アルファに両方を掛けてドーナツ形状にする
                col.a *= alphaMask * outerMask * innerMask;

                return col;
            }
            ENDHLSL
        }
    }
}
