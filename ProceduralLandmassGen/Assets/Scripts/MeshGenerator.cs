using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve, int levelOfDetail)
    {
        AnimationCurve savedHeightCurve = new AnimationCurve(heightCurve.keys);

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
                meshData.Vertices[vertexIndex] = new Vector3(topLeftX + x, savedHeightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);
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

    private Vector3[] CalculateNormals()
    {
        Vector3[] vertexNormals = new Vector3[Vertices.Length];
        int triangleCount = Triangles.Length / 3;
        for (int i = 0; i < triangleCount; i++)
        {
            int normalTriangleIndex = i * 3;
            int vertexIndexA = Triangles[normalTriangleIndex];
            int vertexIndexB = Triangles[normalTriangleIndex + 1];
            int vertexIndexC = Triangles[normalTriangleIndex + 2];

            Vector3 triangleNormal  = SurfaceNormalFromIndices(vertexIndexA, vertexIndexB, vertexIndexC);
            vertexNormals[vertexIndexA] += triangleNormal;
            vertexNormals[vertexIndexB] += triangleNormal;
            vertexNormals[vertexIndexC] += triangleNormal;
        }

        for (int i = 0;i < vertexNormals.Length; i++)
        {
            vertexNormals[i].Normalize();
        }

        return vertexNormals;
    }

    private Vector3 SurfaceNormalFromIndices(int indexA, int indexB, int indexC)
    {
        Vector3 pointA = Vertices[indexA];
        Vector3 pointB = Vertices[indexB];
        Vector3 pointC = Vertices[indexC];

        Vector3 sideAB = pointB - pointA;
        Vector3 sideAC = pointC - pointA;

        return Vector3.Cross(sideAB, sideAC).normalized;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = Vertices;
        mesh.triangles = Triangles;
        mesh.uv = UVs;
        mesh.normals = CalculateNormals();
        return mesh;
    }
}
