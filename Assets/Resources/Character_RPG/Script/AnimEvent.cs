using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent AttackFunc;

    public void OnAttack()
    {
        AttackFunc?.Invoke();
    }
}
