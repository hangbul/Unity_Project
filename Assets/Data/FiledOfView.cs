using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiledOfView : MonoBehaviour
{
    [SerializeField] float ViewAngle = 90.0f;
    [SerializeField] float ViewDistance = 5.0f;
    [SerializeField] int DetailCount = 100;
    public LayerMask crashMask;
    List<Vector3> dirList = new List<Vector3>();
    public MeshFilter myFilter;

    Vector3[] vertexBuf;

    void Start()
    {
        
        Vector3 dir = Quaternion.Euler(0, -ViewAngle * 0.5f, 0) * new Vector3(0, 0, 1);

        dirList.Add(dir);
        float getAngle = ViewAngle / (float)(DetailCount - 1);
        for(int i= 1; i < DetailCount; i++)
        {
            dir = Quaternion.Euler(0,getAngle,0) * dir;
            dirList.Add(dir);
        }

        vertexBuf = new Vector3[DetailCount + 1];
        vertexBuf[0] = Vector3.zero;

        for (int i = 0; i < DetailCount; ++i)
            vertexBuf[i + 1] = vertexBuf[0] + dirList[i] * ViewDistance;

        int[] indexBuf = new int[(DetailCount - 1) * 3];
        for (int i = 0, v = 1 ;i< indexBuf.Length; i+=3, v++)
        {
            indexBuf[i] = 0;
            indexBuf[i + 1] = v;
            indexBuf[i + 2] = v + 1;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertexBuf;
        mesh.triangles = indexBuf;
        myFilter.mesh = mesh;

    }

    Vector3 CalNormal(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        Vector3 d1 = v2 - v1;
        Vector3 d2 = v3 - v1;

        return Vector3.Cross(d1, d2).normalized;
    }

    void FixedUpdate()
    {
        for(int i = 0; i < dirList.Count; i++)
        {
            Ray ray = new Ray(transform.position, transform.rotation * dirList[i]);
            float dist = ViewDistance;

            if(Physics.Raycast(ray, out RaycastHit hit, ViewDistance, crashMask))
               dist = hit.distance;
            vertexBuf[i + 1] = vertexBuf[0] + dirList[i] * dist;

        }myFilter.mesh.vertices = vertexBuf;
    }
}
