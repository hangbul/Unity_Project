using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpgPlayer : CharacterMovement, IBattle
{
    Transform myTarget = null;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            myAnim.SetTrigger("Skill");
        }
    }
    public bool IsLive
    {
        get;
    }

    public void OnMove(Vector3 pos)
    {
        MoveToPos(pos);
    }
    
    public void OnDamage(float dmg)
    {
        _curHP -= dmg;
        if (Mathf.Approximately(curHP, 0.0f))
        {
            Collider[] list = transform.GetComponentsInChildren<Collider>();
            foreach(var col in list)
            {
                col.enabled = false;
            }
            DeathAlarm?.Invoke();
            myAnim.SetTrigger("Death");
        }
        else
        {
            myAnim.SetTrigger("Damage");
        }
    }
    public void OnAttack()
    {
        myTarget.GetComponent<IBattle>()?.OnDamage(35.0f);
    }
    public void BeginBattle(Transform target)
    {
        if(myTarget != null)
        {
            myTarget.GetComponent<CharacterProperty>().DeathAlarm -= TargetDead;
        }
        myTarget = target;
        FollowTarget(myTarget);
        myTarget.GetComponent<CharacterProperty>().DeathAlarm += TargetDead;
    }

    void TargetDead()
    {
        myTarget = null;
        StopAllCoroutines();
    }
}
