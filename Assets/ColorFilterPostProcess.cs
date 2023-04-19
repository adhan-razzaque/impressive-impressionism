using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFilterPostProcess : MonoBehaviour
{
   public Material material;


    void OnRenderImage (RenderTexture source, RenderTexture destination) {
        Graphics.Blit(source, destination, material);
    }
}