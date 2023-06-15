using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer TextureRenderer;

    public void DrawNoiseMap(float[,] noiseMap)
    {
        if (TextureRenderer)
        {
            int width = noiseMap.GetLength(0);
            int height = noiseMap.GetLength(1);

            // Create New Texture2D
            Texture2D texture = new Texture2D(width, height);

            // Generate Pixel Colors for the Texture from NoiseMap
            Color[] colorMap = new Color[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
                }
            }

            // Set TextureMap from Color Array
            texture.SetPixels(colorMap);
            texture.Apply();

            // Updating Renderer
            TextureRenderer.sharedMaterial.mainTexture = texture;
            TextureRenderer.transform.localScale = new Vector3(width, 1, height);
        }
    }
}
