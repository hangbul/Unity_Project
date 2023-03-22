using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private float tumble;
    private GameObject _Earth;

    float Velocity = 0.2f;
    Vector3 targetPos;

    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
        _Earth = GameObject.Find("Earth");
        targetPos = _Earth.transform.position;
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

}