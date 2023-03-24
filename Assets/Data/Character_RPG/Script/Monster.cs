using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : CharacterMovement
{
    public enum State
    {
        Create, Normal, Battle
    }

    public State myState = State.Create;
    Vector3 orgPos;

    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
                StartCoroutine(Wadering(Random.Range(1.0f, 3.0f)));
                break;
            case State.Battle:
                StopAllCoroutines();

                break;
            default:
                Debug.Log("비 처리 상태 등장!");
                break;

        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                break;
            case State.Battle:
                break;
            default:
                Debug.Log("비 처리 상태 등장!");
                break;

        }


    }


    void Start()
    {
        orgPos = transform.position;
        ChangeState(State.Normal);
    }

    void Update()
    {
        StateProcess();
    }

    IEnumerator Wadering(float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 pos = orgPos;
        pos.x += Random.Range(-5.0f, 5.0f);
        pos.z += Random.Range(-5.0f, 5.0f);
        MoveToPos(pos, () => StartCoroutine(Wadering(Random.Range(1.0f, 3.0f))));

    }
}
