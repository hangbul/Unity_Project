using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriAngle : MonoBehaviour
{
    public MeshFilter myFilter;

    void Start()
    {
        Vector3[] vertexBuffer = new Vector3[8];
        vertexBuffer[0] = new Vector3(-0.5f, 0.5f, -0.5f);
        vertexBuffer[1] = new Vector3(0.5f, 0.5f, -0.5f);
        vertexBuffer[2] = new Vector3(0.5f, -0.5f, -0.5f);
        vertexBuffer[3] = new Vector3(-0.5f, -0.5f, -0.5f);
        vertexBuffer[4] = vertexBuffer[5] = vertexBuffer[6] = vertexBuffer[7] = new Vector3(0, 0, 0.5f);

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
        indexBuffer[1] = 2;
        indexBuffer[2] = 3;
        
        indexBuffer[3] = 0;
        indexBuffer[4] = 1;
        indexBuffer[5] = 2;

        indexBuffer[6] = 4;
        indexBuffer[7] = 1;
        indexBuffer[8] = 0;

        indexBuffer[9] = 5;
        indexBuffer[10] = 2;
        indexBuffer[11] = 1;

        indexBuffer[12] = 6;
        indexBuffer[13] = 3;
        indexBuffer[14] = 2;

        indexBuffer[15] = 7;
        indexBuffer[16] = 0;
        indexBuffer[17] = 3;


        

        Mesh mesh = new Mesh();
        mesh.vertices = vertexBuffer;       // vertexBuffer
        mesh.triangles = indexBuffer;      // indexBuffer
        mesh.uv = uvList;

        Vector3[] normalList = new Vector3[8];

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
