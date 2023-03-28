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

    public void OnMove(Vector3 pos)
    {
        MoveToPos(pos);
    }
    
    public void OnDamage(float dmg)
    {
        health -= dmg;
        myAnim.SetFloat("Health", health);
        myAnim.SetTrigger("Damage");
    }
    public void OnAttack()
    {
        myTarget.GetComponent<IBattle>()?.OnDamage(35.0f);
    }
    public void BeginBattle(Transform target)
    {
        myTarget = target;
        FollowTarget(myTarget);
    }
}
