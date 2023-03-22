using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Control_Picking : MonoBehaviour
{
    public LayerMask pickMask;
    public LayerMask enemyMask;

    public bool isFireReady = true, isMoving = false;
    public float MoveSpeed = 10.0f;
    public float Velocity = 5.0f;
    public float RotVelocity = 360.0f;

    public float DegreeSpeed = 1.0f;
    public float dy = 1.0f;
    public float _rotate_limit;

    public float dest_dt = 0.0f;
    public int count = 0;

    public Bomb _bomb;

    public Transform _Cannon_Barrel = null;
    public Transform _Head = null;
    public Transform[] _point = null;

    Vector3 targetPos;
    Vector3 targetRot;

    public Camera main_cam;
    public GameObject orgBomb = null;
    public GameObject _effect = null;
    public GameObject point = null;
    public float Fire_Delay_time = 1.5f;

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
                    StartCoroutine(Rotating(dir));

                }
            }
        }
   
        transform.position = Vector3.Lerp(transform.position, targetPos, Velocity * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRot), RotVelocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isFireReady)
        {
            _bomb.OnFire();
            _bomb = null;
            GameObject obj = Instantiate(orgBomb, _point[count]);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = _point[count].localRotation; ;
            _bomb = obj.GetComponent<Bomb>();
            StartCoroutine(EffectPlay());
        }



        if (Input.GetKey(KeyCode.LeftArrow))
            _Head.Rotate(Vector3.up * DegreeSpeed * Time.deltaTime * -1);
        if (Input.GetKey(KeyCode.RightArrow))
            _Head.Rotate(Vector3.up * DegreeSpeed * Time.deltaTime);


        // Quaternion 값으로 변환시 유니티 상의 표기 값 -180 ~ 180은 0 ~ 360 으로 편환
        if (Input.GetKey(KeyCode.UpArrow))
            _Cannon_Barrel.Rotate(Vector3.left * DegreeSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            _Cannon_Barrel.Rotate(Vector3.right * DegreeSpeed * Time.deltaTime);

        Vector3 angle = _Cannon_Barrel.localRotation.eulerAngles;


        if (angle.x > 180.0f)
            angle.x -= 360.0f;

        angle.x = Mathf.Clamp(angle.x, -60.0f, 10.0f);
 
        _Cannon_Barrel.localRotation = Quaternion.Euler(angle);
    }
    IEnumerator EffectPlay()
    {
        isFireReady = false;

        GameObject obj = Instantiate(_effect, _point[count].position, Quaternion.identity);
        yield return new WaitForSeconds(Fire_Delay_time);
        isFireReady = true;
        count++;
        if (count >= _point.Length)
            count = 0;
        Destroy(obj);
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


            yield return null;
        }
    }
    IEnumerator Rotating(Vector3 dir)
    {
        //유니티 함수 제공
        {
            /*
            //내적시 정규화 필요
            float d = Vector3.Dot(transform.forward, dir);

            float radian = Mathf.Acos(d);       //라디안 값
                                                // y : 180도  = x : pi
                                                // y = 180도 * (radian / pi)
            float angle = 180.0f * radian / Mathf.PI;
             */
        }
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
