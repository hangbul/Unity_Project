using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float directionY = 1.0f;
    Vector3 target;
    Vector3 direction_Vector ;

    Vector3 offset;

    void Start()
    {
        direction_Vector = new Vector3(1, 0, 0);
        offset = this.transform.position;
        target = transform.position + direction_Vector * directionY * 2.5f;

    }

    // Update is called once per fram=e
    void Update()
    {
        //위로 2.5m 올라간 뒤 다시 원래 자리로 돌아오는걸 반복

        //transform.Translate(direction_Vector * directionY * speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (target == transform.position)
        {
            directionY *= -1;
            target = transform.position + direction_Vector * directionY * 2.5f;
        }


    }
}
