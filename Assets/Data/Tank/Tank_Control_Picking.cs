using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Control_Picking : MonoBehaviour
{
    public bool isFireReady = true, isMoving = false;
    public float Speed = 1.0f;
    public float DegreeSpeed = 1.0f;
    public float dy = 1.0f;
    public float _rotate_limit;

    public float dt = 0.0f;

    public int count = 0;

    public Bomb _bomb;

    public Transform _Cannon_Barrel = null;
    public Transform _Head = null;
    public Transform[] _point = null;

    public Camera main_cam;
    public GameObject orgBomb = null;
    public GameObject _effect = null;
    public float Fire_Delay_time = 1.5f;

    public Vector3 startPos, destPos, target_rotate;
    void Start()
    {
        startPos = destPos = transform.position;

        StartCoroutine(Moving());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = main_cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                dt = 0.0f;
                isMoving = true;
                startPos = transform.position;
                destPos = hit.point;
                target_rotate = destPos - startPos;
                StartCoroutine(Moving());
            }
        }

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
    IEnumerator Moving()
    {
        while (isMoving)
        {
            dt = Mathf.Clamp(dt + Time.deltaTime, 0.0f, 20.0f);
            float curr_range = Vector3.Distance(transform.position, destPos);

            //Approximately 근삿값 비교
            if (Mathf.Approximately(dt, 20.0f) || curr_range == 0)
                isMoving = false;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target_rotate), dt);
            transform.position = Vector3.Lerp(startPos, destPos, dt);
            
            yield return null;
        }
    }
}
