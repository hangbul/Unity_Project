using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public LayerMask crashMask;
    public float Rotspeed;
    public float zoomspeed;
    public Transform myTarget;
    Vector3 dir = Vector3.zero;
    float Dist = 0.0f;
    float targetDist = 0.0f;
    private void Awake()
    {
        transform.LookAt(myTarget);
        dir = transform.position - myTarget.position;
        targetDist = Dist = dir.magnitude;
        dir.Normalize();
        dir = transform.InverseTransformDirection(dir);
    }

    // Start is called before the first frame update
    void Start()
    {
            
    }

    Quaternion rotX = Quaternion.identity, rotY = Quaternion.identity;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse Y");
            float y = Input.GetAxis("Mouse X");
            //���ʹϾ�

            rotX *= Quaternion.Euler(x, 0, 0);

            float angle = rotX.eulerAngles.x;
            if (angle > 180.0f)
                angle -= 360.0f;
            angle = Mathf.Clamp(angle, -60.0f, 80.0f);

            rotX = Quaternion.Euler(angle, 0, 0);

            rotY *= Quaternion.Euler(0, y, 0);

            //Quaternion rot = Quaternion.Euler(x, y, 0);
            //transform.forward = -dir;
        }
        targetDist -= Input.GetAxis("Mouse ScrollWheel") * zoomspeed;
        targetDist = Mathf.Clamp(targetDist, 1.0f, 10.0f);

        Dist = Mathf.Lerp(Dist, targetDist, Time.deltaTime * 3.0f);

        Vector3 target_dir = rotY * rotX * dir;
        float radius = 0.5f;
        if (Physics.Raycast(new Ray(myTarget.position, target_dir), out RaycastHit hit, Dist + radius, crashMask))
        {
            //transform.position = hit.point + -target_dir * radius;
            if ((1 << hit.transform.gameObject.layer & crashMask) != 0)
                Dist = hit.distance - radius;
        }
        //transform.position = myTarget.position + target_dir * Dist;
        transform.position = myTarget.position + target_dir * Dist;
        transform.LookAt(myTarget);
    }
}
