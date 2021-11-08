Shader "Custom/Grass"
{
    Properties
    {
        _Color("Color",Color)=(1,1,1,1)
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            float3 worldNormal;//法線ベクトル
            float3 viewDir;//視線ベクトル
        };
        
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //マテリアルの色を白色にする
            o.Albedo = _Color;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            //法線ベクトルと視線ベクトルの内積を取ってきて1から引くことで
            //透明度を平行なら０、垂直なら１にする
            float alpha = 1 - (abs(dot(IN.viewDir, IN.worldNormal)));
            o.Alpha = alpha * 1.5f + 0.1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
