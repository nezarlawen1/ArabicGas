using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer TextureRenderer;
    public MeshFilter MeshFilterRef;
    public MeshRenderer MeshRendererRef;

    public void DrawTexture(Texture2D texture)
    {
        if (TextureRenderer)
        {
            TextureRenderer.sharedMaterial.mainTexture = texture;
            TextureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
        }
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        MeshFilterRef.sharedMesh = meshData.CreateMesh();
        MeshRendererRef.sharedMaterial.mainTexture = texture;
    }
}
