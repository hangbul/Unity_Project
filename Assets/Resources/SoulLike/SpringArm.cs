using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Zoomdata
{
    public Vector2 ZoomRange;
    public float ZoomSpeed;
    public float LerpSpeed;
    public float curDist;
    public float desirDist;
    
    public Zoomdata(float speed)
    {
        ZoomSpeed = speed;
        ZoomRange = new Vector2(1.0f, 7.0f);
        LerpSpeed = 3.0f;
        curDist = 0.0f;
        desirDist = 0.0f;
    }
}
public class SpringArm : MonoBehaviour
{
    float Offset = 0.5f;
    public float RotSpeed = 180.0f;
    [SerializeField]
    private LayerMask crashMask;
    public Zoomdata myZoomdata = new Zoomdata(5.0f);
    Transform myCam = null;
    Vector2 LookUpRange = new Vector2(-60, 90);
    Vector3 curRot = Vector3.zero;

    void Start()
    {
        myCam = GetComponentInChildren<Camera>().transform;
        curRot = transform.localRotation.eulerAngles;
        myZoomdata.curDist = myCam.localPosition.magnitude;
        myZoomdata.desirDist = myZoomdata.curDist;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            curRot.x = Mathf.Clamp(curRot.x - Input.GetAxis("Mouse Y") * RotSpeed * Time.deltaTime, LookUpRange.x, LookUpRange.y);
            curRot.y += Input.GetAxis("Mouse X") * RotSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(curRot.x, 0, 0);
            transform.parent.localRotation = Quaternion.Euler(0, curRot.y, 0);
        }
        myZoomdata.desirDist = Mathf.Clamp(myZoomdata.desirDist + Input.GetAxis("Mouse ScrollWheel") * myZoomdata.ZoomSpeed, myZoomdata.ZoomRange.x, myZoomdata.ZoomRange.y);

        if(Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, myZoomdata.desirDist + Offset, crashMask))
        {
            myZoomdata.curDist = hit.distance - Offset;
        }
        else
        {
            myZoomdata.curDist = Mathf.Lerp(myZoomdata.curDist, myZoomdata.desirDist, Time.deltaTime * myZoomdata.LerpSpeed);
        }
        myCam.localPosition = Vector3.back * myZoomdata.curDist;

    }
}
