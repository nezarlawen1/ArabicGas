using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve, int levelOfDetail)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        int meshSimplifyIncrement = (levelOfDetail == 0) ? 1 : levelOfDetail * 2;
        int vertsPerLine = (width - 1) / meshSimplifyIncrement + 1;

        MeshData meshData = new MeshData(vertsPerLine, vertsPerLine);
        int vertexIndex = 0;

        for (int y = 0; y < height; y += meshSimplifyIncrement)
        {
            for (int x = 0; x < width; x += meshSimplifyIncrement)
            {
                meshData.Vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);
                meshData.UVs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);
                //meshData.UVs[vertexIndex] = new Vector2((width - 1 - x) / (float)width, y / (float)height);


                if (x < width - 1 && y < height - 1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + vertsPerLine + 1, vertexIndex + vertsPerLine);
                    meshData.AddTriangle(vertexIndex + vertsPerLine + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        return meshData;
    }
}

public class MeshData
{
    public Vector3[] Vertices;
    public int[] Triangles;
    public Vector2[] UVs;

    int triangleIndex;

    public MeshData(int width, int height)
    {
        Vertices = new Vector3[width * height];
        Triangles = new int[(width - 1) * (height - 1) * 6];
        UVs = new Vector2[width * height];
    }

    public void AddTriangle(int a, int b, int c)
    {
        Triangles[triangleIndex] = a;
        Triangles[triangleIndex + 1] = b;
        Triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = Vertices;
        mesh.triangles = Triangles;
        mesh.uv = UVs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
