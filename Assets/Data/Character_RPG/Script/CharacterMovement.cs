using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : CharacterProperty
{
    protected void MoveToPos(Vector3 pos, UnityAction done = null)
    {
        StopAllCoroutines();
        StartCoroutine(MovingToPos(pos, done));
    }
    IEnumerator MovingToPos(Vector3 pos, UnityAction done)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        StartCoroutine(Rotating(dir));

        myAnim.SetBool("isMoving", true);

        while (dist > 0.0f)
        {
            if (!myAnim.GetBool("isAttacking"))
            {

                float delta = MoveSpeed * Time.deltaTime;
                if (dist - delta < 0.0f)
                {
                    delta = dist;
                }
                dist -= delta;
                transform.Translate(dir * delta, Space.World);
            }
            yield return null;
        }

        myAnim.SetBool("isMoving", false);
        done?.Invoke();
    }

    IEnumerator Rotating(Vector3 dir)
    {
        float angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -1.0f;
        }
        while (angle > 0.0f)
        {
            float delta = RotSpeed * Time.deltaTime;
            if (angle - delta < 0.0f)
            {
                delta = angle;
            }
            angle -= delta;
            transform.Rotate(Vector3.up * rotDir * delta);
            yield return null;
        }
    }

}
