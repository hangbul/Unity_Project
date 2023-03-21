using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    
    //[SerializeField] float directionY = 1.0f;

    Vector3 direction_Vector ;


    //03/21
    Vector3 startPos, destPos;
    float Range = 2.0f;
    float t = 0.0f;
    float Dir_t = 1.0f;

    //case2
    IEnumerator coFunc;

    void Start()
    {
        t = 0.5f;

        direction_Vector = new Vector3(1, 0, 0);
        startPos = destPos = transform.position;

        startPos.x -= Range / 2.0f;
        destPos.x += Range / 2.0f;

        StartCoroutine(Moving());
        //case2
        /*coFunc = Moving();*/
    }

    // Update is called once per fram=e
    void Update()
    {
        //case2
        /*coFunc.MoveNext();*/

        //위로 2.5m 올라간 뒤 다시 원래 자리로 돌아오는걸 반복

        //transform.Translate(direction_Vector * directionY * speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F1))
        {
            //StopAllCoroutines();//해당 스크립트와 연관된(부모, 자식 등) 코루틴을 종료
            StopCoroutine(Moving());
        }

    }

    IEnumerator Moving()
    {
        /*
         여기에 지역변수를 선언해도 문제 없음
         */
        while (true)
        {
            // 03/21 선형 보간
            // t += Dir_t * Time.deltaTime;
            t = Mathf.Clamp(t + Dir_t * Time.deltaTime, 0.0f, 1.0f);

            //Approximately 근삿값 비교
            if (Mathf.Approximately(t, 0.0f) || Mathf.Approximately(t, 1.0f))
                Dir_t *= -1;

            transform.position = Vector3.Lerp(startPos, destPos, t * speed);
            yield return null;
        }
    }

}
