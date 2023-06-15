using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer TextureRenderer;

    public void DrawTexture(Texture2D texture)
    {
        if (TextureRenderer)
        {
            TextureRenderer.sharedMaterial.mainTexture = texture;
            TextureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
        }
    }
}
