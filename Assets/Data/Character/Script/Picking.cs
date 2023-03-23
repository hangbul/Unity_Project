using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picking : MonoBehaviour
{
    public LayerMask pickMask;
    public LayerMask enemyMask;

    public float MoveSpeed = 10.0f;
    public float Velocity = 5.0f;
    public float RotVelocity = 360.0f;

    public float DegreeSpeed = 1.0f;
    public float dy = 1.0f;
    public float _rotate_limit;

    public float dest_dt = 0.0f;

    Vector3 targetPos;
    Vector3 targetRot;

    public Camera main_cam;

    void Start()
    {
        targetPos = transform.position;
        targetRot = transform.rotation.eulerAngles;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = main_cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, pickMask))
            {

                if (((1 << hit.transform.gameObject.layer) & enemyMask) == 0)
                {
                    Vector3 dir = hit.point - transform.position;

                    StopAllCoroutines();

                    StartCoroutine(Moving(dir));

                }
            }
        }
   
        transform.position = Vector3.Lerp(transform.position, targetPos, Velocity * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRot), RotVelocity * Time.deltaTime);

    }

    IEnumerator Moving(Vector3 dir)
    {
        float dist = dir.magnitude; //Vector3.Distance(pos, trasform.position);
        dir.Normalize();        // normalized

        

        while (dist > 0.0f)
        {
            float delta = 2.0f * Time.deltaTime;        //length
            
            if(dist - delta < 0.0f)
                delta = dist;

            //transform.Translate(dir * delta * MoveSpeed, Space.World);
            targetPos += dir * delta;

            dist -= delta;


            yield return StartCoroutine(Rotating(dir));
        }
    }
    IEnumerator Rotating(Vector3 dir)
    {
        dir.Normalize();        // normalized
        float angle = Vector3.Angle(transform.forward, dir);

        float rotdir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0)
            rotdir = -1.0f;
        
        //transform.Rotate(Vector3.up * angle * rotdir);
        while (angle > 0.0f)
        {
            float delta = RotVelocity * Time.deltaTime;
            if (angle - delta < 0.0f)
                delta = angle;

            //targetRot.Rotate(Vector3.up * rotdir * delta);
            targetRot.y += delta * rotdir;
            angle -= delta;

            yield return null;
        }
    }
}
