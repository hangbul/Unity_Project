using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIMovement : CharacterMovement
{
    protected void MoveByPath(Vector3[] pathlList)
    {
        StopAllCoroutines();
        StartCoroutine(MovingByPath(pathlList));
    }

    IEnumerator MovingByPath(Vector3[] pathlList)
    {
        myAnim.SetFloat("Speed", MoveSpeed);
        for (int i=1; i < pathlList.Length;)
        {
            

            bool done = false;
            MoveToPos(pathlList[i], () => done = true);
            while (!done)
            {
                for (int n = i; n < pathlList.Length; n++)
                {
                    Debug.DrawLine(n == i ? transform.position : pathlList[n - 1], pathlList[n], Color.red);
                }
                yield return null;
            }
            i++;
        }
        myAnim.SetFloat("Speed", 0);

    }


}
