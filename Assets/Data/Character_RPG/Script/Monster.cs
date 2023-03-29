using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Monster : CharacterMovement, IPerception, IBattle
{
 
    public enum State
    {
        Create, Normal, Battle, Death
    }



    public State myState = State.Create;
    Vector3 orgPos;
    public Transform myTarget = null;

    public bool IsLive
    {
        get => myState != State.Death;
    }

    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
                myAnim.SetBool("isMoving", false);
                StopAllCoroutines();
                StartCoroutine(Wadering(Random.Range(1.0f, 3.0f)));
                break;
            case State.Battle:
                StopAllCoroutines();
                FollowTarget(myTarget);
                break;
            case State.Death:
                DeathAlarm?.Invoke();
                StopAllCoroutines();
                myAnim.SetTrigger("Death");
                break;
            default:
                Debug.Log("ó�� ���� �ʴ� ���� �Դϴ�.");
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
            case State.Death:
                //this.transform.GetComponent<Monster>().enabled = false;
                break;
            default:
                Debug.Log("�� ó�� ���� ����!");
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
    //Interface �Լ��� ������ public ����
    public void Find(Transform target)
    {
        myTarget = target;
        myTarget.GetComponent<CharacterProperty>().DeathAlarm += () => ChangeState(State.Normal);
        ChangeState(State.Battle);
    }

    public void LostTarget()
    {
        myTarget = null;
        ChangeState(State.Normal);
    }
    //�������̽� ���� Ŀ�ø� ���� ����
    public void OnAttack()
    {
        myTarget.GetComponent<IBattle>()?.OnDamage(AttackPoint);
    }
    public void OnDamage(float dmg)
    {
        _curHP -= dmg;
        if (Mathf.Approximately(_curHP, 0.0f))
            ChangeState(State.Death);
        myAnim.SetTrigger("Damage");
    }
}
