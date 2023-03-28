using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProperty : MonoBehaviour
{
    public float MoveSpeed = 2.0f;
    public float RotSpeed = 360.0f;
    public float AttackRange = 1.0f;
    public float AttackDelay = 1.0f;
    public float health = 100.0f;
    protected float playTime = 0.0f;

    Animator _anim = null;
    protected Animator myAnim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
                if (_anim == null)
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }
}
