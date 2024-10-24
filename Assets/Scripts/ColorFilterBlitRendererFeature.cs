﻿using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

internal class ColorFilterBlitRendererFeature : ScriptableRendererFeature
{
    public Shader m_Shader;

    public float red_orange = 0.55f;
    public float yellow_green = 0.8f;
    public float blue_indigo = 0.7f;
    public float violet_fuschia = 0.4f;

    public float add_red;
    public float add_green;
    public float add_blue;

    private Material m_Material;

    private ColorFilterBlitPass m_RenderPass = null;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (renderingData.cameraData.cameraType != CameraType.Game) return;

        renderer.EnqueuePass(m_RenderPass);
    }

    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
        if (renderingData.cameraData.cameraType != CameraType.Game) return;

        // Calling ConfigureInput with the ScriptableRenderPassInput.Color argument
        // ensures that the opaque texture is available to the Render Pass.
        m_RenderPass.ConfigureInput(ScriptableRenderPassInput.Color);
        var settings = new ColorFilterBlitSettings
        {
            red_orange = red_orange,
            yellow_green = yellow_green,
            violet_fuschia = violet_fuschia,
            blue_indigo = blue_indigo,
            
            add_red = add_red,
            add_green = add_green,
            add_blue = add_blue,
        };
        m_RenderPass.SetTarget(renderer.cameraColorTargetHandle, settings);
    }

    public override void Create()
    {
        m_Material = CoreUtils.CreateEngineMaterial(m_Shader);
        m_RenderPass = new ColorFilterBlitPass(m_Material);
    }

    protected override void Dispose(bool disposing)
    {
        CoreUtils.Destroy(m_Material);
    }
}