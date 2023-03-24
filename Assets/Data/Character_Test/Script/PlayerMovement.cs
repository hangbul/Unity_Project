using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerProperty
{
    protected void MoveToPos(Vector3 pos)
    {
        StopAllCoroutines();
        StartCoroutine(Moving(pos));
    }
    protected void Shoot(Vector3 pos)
    {
        StopAllCoroutines();
        StartCoroutine(Shooting(pos));
    }


    IEnumerator Shooting(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        StartCoroutine(Rotating(dir));

        myAnim.SetBool("isShooting", true);
        yield return new WaitForSeconds(1.0f);
        myAnim.SetBool("isShooting", false);
    }

    IEnumerator Moving(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        StartCoroutine(Rotating(dir));
        myAnim.SetBool("isMoving", true);
        while (dist > 0.0f) {
            float delta = Time.deltaTime * MoveSpeed;
            if(dist - delta < 0.0f)
            {
                delta = dist;
            }

            dist -= delta;
            transform.Translate(dir * delta, Space.World);

            yield return null;
        }
        myAnim.SetBool("isMoving", false);
    }
    IEnumerator Rotating(Vector3 dir)
    {
        float angle = Vector3.Angle(transform.forward, dir);
        float rotdir = 1.0f;

        if(Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotdir = -1.0f;
        }
        while(angle > 0.0f)
        {
            float delta = Time.deltaTime * RotSpeed;
            if(angle - delta < 0.0f)
            {
                delta = angle;
            }

            angle -= delta;
            transform.Rotate(Vector3.up * rotdir * delta);
            yield return null;
        }

    }
}