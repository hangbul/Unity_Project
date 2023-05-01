using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent AttackFunc;
    public UnityEvent DeadFunc;
    public UnityEvent ComoboCheckStart;
    public UnityEvent ComoboCheckEnd;

    public void OnAttack()
    {
        AttackFunc?.Invoke();
    }

    public void OnDead()
    {

    }

    public void ComboCheckStart()
    {
        ComoboCheckStart?.Invoke();
    }
    public void ComboCheckEnd()
    {
        ComoboCheckEnd?.Invoke();
    }
}
