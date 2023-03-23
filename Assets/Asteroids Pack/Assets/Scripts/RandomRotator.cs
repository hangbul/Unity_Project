using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private float tumble;

    public GameObject _effect;
    public Transform myTarget;

    private bool isQuitting = false;
    float Velocity = 0.2f;


    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;

        myTarget = Earth.Instance._myEarth;

        Vector3 dir = myTarget.position- transform.position;
        StartCoroutine(Moving(dir));
    }
    private void Update()
    {
        if(myTarget!= null)
            transform.position = Vector3.Lerp(transform.position, myTarget.position, Velocity * Time.deltaTime);
    }

    IEnumerator Moving(Vector3 dir)
    {
        float dist = dir.magnitude; //Vector3.Distance(pos, trasform.position);
        dir.Normalize();        // normalized

        while (dist > 0.0f)
        {
            float delta = 2.0f * Time.deltaTime;        //length

            if (dist - delta < 0.0f)
                delta = dist;

            //transform.Translate(dir * delta * MoveSpeed, Space.World);
            transform.position += dir * delta;

            dist -= delta;


            yield return null;
        }
        Destroy(gameObject);
    }
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    
    private void OnDestroy() //이벤트 함수 중 1 : 파괴될 때 실행
    {
        if (!isQuitting)
        {
            GameObject obj = Instantiate(_effect, this.transform.position, Quaternion.identity); return;
        }
    }

    
}