using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;


public class Monster : CharacterMovement, IPerception, IBattle
{
 


    public enum State
    {
        Create, Normal, Battle, Death
    }



    public State myState = State.Create;
    Vector3 orgPos;
    public Transform myTarget = null;
    UnityAction deadAction = null;

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
                Collider[] list = transform.GetComponentsInChildren<Collider>();
                foreach (var col in list)
                {
                    col.enabled = false;
                }
                DeathAlarm?.Invoke();
                StopAllCoroutines();
                myAnim.SetTrigger("Death");
                StartCoroutine(DeadProtocol());
                break;
            default:
                Debug.Log("처리 되지 않는 상태 입니다.");
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

        MiniMapIcon icon = (Instantiate(Resources.Load("UI/MiniMapIcon"), SceneData.Inst.miniMap) as GameObject).GetComponent<MiniMapIcon>();
        icon.Initialize(transform, Color.red);
    }

    void Update()
    {
        StateProcess();
    }
    IEnumerator DeadProtocol()
    {
        yield return new WaitForSeconds(4.0f);

        float dist = 1.0f;
        while (dist > 0.0f) {
            float delta = Time.deltaTime * 0.4f;
            if (dist - delta < 0.0f)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(Vector3.down * delta, Space.World);
            yield return null ;
        }
        deadAction?.Invoke();
        Destroy(this.transform.gameObject);
    }
    IEnumerator Wadering(float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 pos = orgPos;
        pos.x += Random.Range(-5.0f, 5.0f);
        pos.z += Random.Range(-5.0f, 5.0f);
        MoveToPos(pos, () => StartCoroutine(Wadering(Random.Range(1.0f, 3.0f))));

    }
    //Interface 함수는 무조건 public 변형
    public void Find(Transform target)
    {
        myTarget = target;
        myTarget.GetComponent<CharacterProperty>().DeathAlarm += () => { if (IsLive) ChangeState(State.Normal); };
        ChangeState(State.Battle);
    }

    public void LostTarget()
    {
        myTarget = null;
        ChangeState(State.Normal);
    }
    //인터페이스 사용시 커플링 방해 가능
    public void OnAttack()
    {
        if(IsLive)
            myTarget.GetComponent<IBattle>()?.OnDamage(AttackPoint);
    }
    public void OnDamage(float dmg)
    {
        curHP -= dmg;
        if (Mathf.Approximately(curHP, 0.0f))
            ChangeState(State.Death);
        myAnim.SetTrigger("Damage");
    }
}
