using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Control : MonoBehaviour
{
    public float Speed = 1.0f;
    public float DegreeSpeed = 1.0f;
    public float dy = 1.0f;

    public float _rotate_limit;

    public Transform _Cannon_Barrel = null;
    public Transform _Head = null;

    void Start()
    {
    }

    void Update()
    {

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


        if (Input.GetKey(KeyCode.LeftArrow))
            _Head.Rotate(Vector3.up * DegreeSpeed * Time.deltaTime * -1);
        if (Input.GetKey(KeyCode.RightArrow))
            _Head.Rotate(Vector3.up * DegreeSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.UpArrow))
            _Cannon_Barrel.Rotate(Vector3.left * DegreeSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            _Cannon_Barrel.Rotate(Vector3.right * DegreeSpeed * Time.deltaTime);
    }
}
