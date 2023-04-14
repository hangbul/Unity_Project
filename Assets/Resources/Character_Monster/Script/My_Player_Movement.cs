using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class My_Player_Movement : My_Player_Property
{
    public void MoveToPos(Vector3 pos, UnityAction act = null)
    {
        StopAllCoroutines();
        StartCoroutine(Moving(pos, act));
    }

    IEnumerator Moving(Vector3 pos, UnityAction act)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        StartCoroutine(Rotating(dir));

        _MyAnim.SetBool("isMoving", true);

        while(dist > 0.0f)
        {
            float delta = Speed * Time.deltaTime;
            if(dist -delta < 0.0f)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(dir * delta, Space.World);

            yield return null;
        }

        _MyAnim.SetBool("isMoving", false);
        act?.Invoke();
    }

    IEnumerator Rotating(Vector3 dir)
    {
        float angle = Vector3.Angle(transform.forward, dir);
        float rot_dir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f)
            rot_dir = -1.0f;

        while (angle>0)
        {
            float delta = RotSpeed * Time.deltaTime;
            if (angle - delta < 0.0f)
            {
                delta = angle;
            }
            angle -= delta;
            transform.Rotate(Vector3.up * rot_dir * delta);
            yield return null;
        }
    }
}
