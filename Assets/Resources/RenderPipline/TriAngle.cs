using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriAngle : MonoBehaviour
{
    public MeshFilter myFilter;

    void Start()
    {
        Vector3[] vertexBuffer = new Vector3[16];

        vertexBuffer[0] = vertexBuffer[6] = vertexBuffer[14] = new Vector3(-0.5f, -0.5f, 0.5f);
        vertexBuffer[1] = vertexBuffer[5] = vertexBuffer[9] = new Vector3(0.5f, -0.5f, 0.5f);
        vertexBuffer[2] = vertexBuffer[8] = vertexBuffer[12] = new Vector3(0.5f, -0.5f, -0.5f);
        vertexBuffer[3] = vertexBuffer[11] = vertexBuffer[15] = new Vector3(-0.5f, -0.5f, -0.5f);
        
        
        
        vertexBuffer[4] = vertexBuffer[7] = vertexBuffer[10] = vertexBuffer[13] = new Vector3(0, 0, 0);

        Vector2[] uvList = new Vector2[8];
        uvList[0] = new Vector2(0.25f, 0.75f);
        uvList[1] = new Vector2(0.75f, 0.75f);
        uvList[2] = new Vector2(0.75f, 0.35f);
        uvList[3] = new Vector2(0.25f, 0.35f);
        uvList[4] = new Vector2(0.5f, 1);
        uvList[5] = new Vector2(1, 0.5f);
        uvList[6] = new Vector2(0.5f, 0);
        uvList[7] = new Vector2(0, 0.5f);

        int[] indexBuffer = new int[18];

        indexBuffer[0] = 0;
        indexBuffer[1] = 3;
        indexBuffer[2] = 2;
        
        indexBuffer[3] = 0;
        indexBuffer[4] = 2;
        indexBuffer[5] = 1;

        indexBuffer[6] = 4;
        indexBuffer[7] = 6;
        indexBuffer[8] = 5;

        indexBuffer[9] = 7;
        indexBuffer[10] = 9;
        indexBuffer[11] = 8;

        indexBuffer[12] = 10;
        indexBuffer[13] = 12;
        indexBuffer[14] = 11;

        indexBuffer[15] = 13;
        indexBuffer[16] = 15;
        indexBuffer[17] = 14;


        

        Mesh mesh = new Mesh();
        mesh.vertices = vertexBuffer;       // vertexBuffer
        mesh.triangles = indexBuffer;      // indexBuffer
        mesh.uv = uvList;

        Vector3[] normalList = new Vector3[16];

        for(int i=0; i<indexBuffer.Length;i+= 3)
        {
            normalList[indexBuffer[i]] = normalList[indexBuffer[i + 1]] = normalList[indexBuffer[i + 2]] =
                CalNormal(vertexBuffer[indexBuffer[i]], vertexBuffer[indexBuffer[i + 1]], vertexBuffer[indexBuffer[i + 2]]); 
        }


        mesh.normals = normalList; 

        myFilter.mesh = mesh;
    }

    Vector3 CalNormal(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        Vector3 d1 = v2 - v1;
        Vector3 d2 = v3 - v1;

        return Vector3.Cross(d1, d2).normalized;
    }

    void Update()
    {
        
    }
}
