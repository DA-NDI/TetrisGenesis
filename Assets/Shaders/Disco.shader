Shader "Brush/Disco" {
  Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
    _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
    _MainTex ("Base (RGB) TransGloss (A)", 2D) = "white" {}
    _BumpMap ("Normalmap", 2D) = "bump" {}
  }
  SubShader {
    Cull Back
    CGPROGRAM
    #pragma target 3.0
    #pragma surface surf StandardSpecular vertex:vert noshadow
    #pragma multi_compile __ AUDIO_REACTIVE
    #pragma multi_compile __ TBT_LINEAR_TARGET
    #include "./Brush.cginc"

    struct Input {
      float2 uv_MainTex;
      float2 uv_BumpMap;
      float4 color : Color;
      float3 worldPos;
    };

    sampler2D _MainTex;
    sampler2D _BumpMap;
    fixed4 _Color;
    half _Shininess;

    void vert (inout appdata_full v) {
      v.color = TbVertToNative(v.color);
      float t, uTileRate, waveIntensity;

      float radius = v.texcoord.z;

#ifdef AUDIO_REACTIVE
      t = _BeatOutputAccum.z * 5;
      uTileRate = 5;
      waveIntensity = (_PeakBandLevels.y * .8 + .5);
      float waveform = tex2Dlod(_WaveFormTex, float4(v.texcoord.x * 2, 0, 0, 0)).b - .5f;
      v.vertex.xyz += waveform * v.normal.xyz * .2;
#else
      t = _Time.z;
      uTileRate = 10;
      waveIntensity = .6;
#endif
      // Ensure the t parameter wraps (1.0 becomes 0.0) to avoid cracks at the seam.
      float theta = fmod(v.texcoord.y, 1);
      v.vertex.xyz += pow(1 -(sin(t + v.texcoord.x * uTileRate + theta * 10) + 1),2)
              * v.normal.xyz * waveIntensity
              * radius;
    }

    // Input color is _native_
    void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
      fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
      o.Albedo = tex.rgb * _Color.rgb * IN.color.rgb;
      o.Smoothness = _Shininess;
      o.Specular = _SpecColor * IN.color.rgb;
      o.Normal =  float3(0,0,1);

      // XXX need to convert world normal to tangent space normal somehow...
      float3 worldNormal = normalize(cross(ddy(IN.worldPos), ddx(IN.worldPos)));
      o.Normal = -cross(cross(o.Normal, worldNormal), worldNormal);
      o.Normal = normalize(o.Normal);

      // Add a fake "disco ball" hot spot
      float fakeLight = pow( abs(dot(worldNormal, float3(0,1,0))),100);
      o.Emission = IN.color.rgb * fakeLight * 200;
    }
    ENDCG
  }

  FallBack "Transparent/Cutout/VertexLit"
}