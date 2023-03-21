using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Control : MonoBehaviour
{
    
    public float Speed = 1.0f;
    public float DegreeSpeed = 1.0f;
    public float dy = 1.0f;
    public float _rotate_limit;

    public Bomb _bomb;

    public Transform _Cannon_Barrel = null;
    public Transform _Head = null;
    public Transform _point = null;

    public GameObject orgBomb = null;

    void Start()
    {
        
    }
    //�ݶ��̴� ����
    // sphere , box , mesh
    // RigidBody. kinemetic = �� �ǿ��� ��󿡰Ը� ���� ����, �ڽ��� ���� ����
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _bomb.OnFire();
            _bomb = null;
            GameObject obj = Instantiate(orgBomb, _point);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation= _point.localRotation; ;
            _bomb = obj.GetComponent<Bomb>();

        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            dy = 1.0f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Speed * Time.deltaTime);
            dy = -1.0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * DegreeSpeed * Time.deltaTime * -1);

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * DegreeSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R))
        {
            transform.position = Vector3.zero + new Vector3(0, 3.0f, 0);
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }


        if (Input.GetKey(KeyCode.LeftArrow))
            _Head.Rotate(Vector3.up * DegreeSpeed * Time.deltaTime * -1);
        if (Input.GetKey(KeyCode.RightArrow))
            _Head.Rotate(Vector3.up * DegreeSpeed * Time.deltaTime);


        // Quaternion ������ ��ȯ�� ����Ƽ ���� ǥ�� �� -180 ~ 180�� 0 ~ 360 ���� ��ȯ
        if (Input.GetKey(KeyCode.UpArrow))
            _Cannon_Barrel.Rotate(Vector3.left * DegreeSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            _Cannon_Barrel.Rotate(Vector3.right * DegreeSpeed * Time.deltaTime);

        Vector3 angle = _Cannon_Barrel.localRotation.eulerAngles;

       
        if (angle.x > 180.0f)
            angle.x -= 360.0f;
       
        angle.x = Mathf.Clamp(angle.x, -60.0f, 10.0f);
        /*
        if (angle.x > 10.0f)
            angle.x = 10.0f;

        if (angle.x < -60.0f)
            angle.x = -60.0f;
         */

        _Cannon_Barrel.localRotation = Quaternion.Euler(angle);
    }
}
