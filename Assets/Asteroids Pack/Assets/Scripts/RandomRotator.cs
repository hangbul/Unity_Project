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
    
    private bool isQuitting = false;
    float Velocity = 0.2f;
    Vector3 targetPos;

    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
        if(Earth._myEarth != null)
            targetPos = Earth._myEarth.transform.position;

        Vector3 dir = targetPos - transform.position;
        StartCoroutine(Moving(dir));
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Velocity * Time.deltaTime);
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
            targetPos += dir * delta;

            dist -= delta;


            yield return null;
        }
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