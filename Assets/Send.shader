
Shader "URP/SandShaderFullCustom" {
    Properties {
        [Header(Base Settings)]
        _SandColor ("Sand Color", Color) = (0.94, 0.84, 0.65, 1)
        
        [Header(Wave Settings)]
        _WaveFrequency ("Wave Frequency", Range(0.1, 10)) = 3.0
        _WaveSpeed ("Wave Speed", Range(0, 5)) = 1.5
        _WaveIntensity ("Wave Intensity", Range(0, 0.5)) = 0.1
        _WaveDirection ("Wave Direction", Vector) = (1, 0, 0.5, 0)
        
        [Header(Wind Settings)]
        _WindStrength ("Wind Strength", Range(0, 1)) = 0.3
        _WindSpeed ("Wind Speed", Range(0, 5)) = 2.0
        _WindTurbulence ("Wind Turbulence", Range(0, 1)) = 0.2
        
        [Header(Detail Settings)]
        _DetailScale ("Detail Scale", Range(0.1, 50)) = 15.0
        _DetailIntensity ("Detail Intensity", Range(0, 1)) = 0.5
        _GrainSize ("Grain Size", Range(0, 100)) = 50.0
        _GrainIntensity ("Grain Intensity", Range(0, 0.3)) = 0.05
        
        [Header(Lighting Settings)]
        _Smoothness ("Smoothness", Range(0, 1)) = 0.3
        _Metallic ("Metallic", Range(0, 1)) = 0.1
        _SpecularPower ("Specular Power", Range(1, 128)) = 32.0
        _ShadowIntensity ("Shadow Intensity", Range(0, 1)) = 0.5
    }

    SubShader {
        Tags { 
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry"
        }

        Pass {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct Varyings {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float4 shadowCoord : TEXCOORD2;
            };

            CBUFFER_START(UnityPerMaterial)
                float4 _SandColor;
                float _WaveFrequency;
                float _WaveSpeed;
                float _WaveIntensity;
                float4 _WaveDirection;
                float _WindStrength;
                float _WindSpeed;
                float _WindTurbulence;
                float _DetailScale;
                float _DetailIntensity;
                float _GrainSize;
                float _GrainIntensity;
                float _Smoothness;
                float _Metallic;
                float _SpecularPower;
                float _ShadowIntensity;
            CBUFFER_END

            float random(float2 uv) {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            float3 calculateNormal(float3 pos) {
                float eps = 0.02;
                float hL = random(pos.xz * _DetailScale + float2(-eps, 0));
                float hR = random(pos.xz * _DetailScale + float2(eps, 0));
                float hD = random(pos.xz * _DetailScale + float2(0, -eps));
                float hU = random(pos.xz * _DetailScale + float2(0, eps));
                return normalize(float3(hL - hR, 2.0 * eps, hD - hU));
            }

            Varyings vert(Attributes IN) {
                Varyings OUT;
                
                // Вершинные трансформации
                float3 posWS = TransformObjectToWorld(IN.positionOS.xyz);
                
                // Волны
                float2 waveDir = normalize(_WaveDirection.xy);
                float wave = sin(dot(posWS.xz, waveDir) * _WaveFrequency + _Time.y * _WaveSpeed) * _WaveIntensity;



                float windTime = _Time.y * _WindSpeed;
                float2 windOffset = float2(
                    random(posWS.xz + windTime) * _WindTurbulence,
                    random(posWS.zx + windTime) * _WindTurbulence
                ) * _WindStrength;
                
                posWS.xz += windOffset;
                posWS.y += wave;

                // Нормали
                float3 proceduralNormal = calculateNormal(posWS);
                float3 objNormal = TransformObjectToWorldNormal(IN.normalOS);
                OUT.normalWS = lerp(objNormal, proceduralNormal, _DetailIntensity);

                OUT.positionWS = posWS;
                OUT.positionCS = TransformWorldToHClip(posWS);
                OUT.shadowCoord = TransformWorldToShadowCoord(posWS);
                
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target {
                // Освещение
                Light mainLight = GetMainLight(IN.shadowCoord);
                float3 lightDir = mainLight.direction;
                float shadow = lerp(1.0, mainLight.shadowAttenuation, _ShadowIntensity);
                
                // Диффузное освещение
                float NdotL = saturate(dot(IN.normalWS, lightDir)) * shadow;
                float3 diffuse = mainLight.color * NdotL;
                
                // Спекулярные блики
                float3 viewDir = normalize(GetWorldSpaceViewDir(IN.positionWS));
                float3 reflectDir = reflect(-lightDir, IN.normalWS);
                float specular = pow(saturate(dot(viewDir, reflectDir)), _SpecularPower) * _Metallic;
                
                // Детализация и зернистость
                float2 detailUV = IN.positionWS.xz / _DetailScale;
                float pattern = random(detailUV);
                float grain = random(IN.positionWS.xz * _GrainSize) * _GrainIntensity;
                
                // Финальный цвет
                float3 finalColor = _SandColor.rgb * pattern + grain;
                float3 ambient = SampleSH(IN.normalWS);
                float3 lighting = (diffuse + ambient + specular) * finalColor;

               // Полная инициализация SurfaceData
                SurfaceData surfaceData;
                ZERO_INITIALIZE(SurfaceData, surfaceData); // Важно: обнуление структуры
                
                surfaceData.albedo = lighting;
                surfaceData.metallic = _Metallic;
                surfaceData.specular = 0.5;
                surfaceData.smoothness = _Smoothness;
                surfaceData.occlusion = 1.0;
                surfaceData.emission = 0.0;
                surfaceData.alpha = 1.0;
                surfaceData.clearCoatMask = 0.0;
                surfaceData.clearCoatSmoothness = 0.0;
                surfaceData.normalTS = float3(0, 0, 1); // Добавляем нормаль по умолчанию

                // Инициализация InputData
                InputData inputData = (InputData)0;
                inputData.positionWS = IN.positionWS;
                inputData.normalWS = IN.normalWS;
                inputData.viewDirectionWS = viewDir;
                inputData.shadowCoord = IN.shadowCoord;
                inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(IN.positionCS);
                inputData.shadowMask = half4(1, 1, 1, 1);
                inputData.fogCoord = 0;
                inputData.vertexLighting = half3(0, 0, 0);
                inputData.bakedGI = SampleSH(IN.normalWS);

                return UniversalFragmentPBR(inputData, surfaceData);
            }
            ENDHLSL
        }

        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
    }
}