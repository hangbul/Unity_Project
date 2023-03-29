using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProperty : MonoBehaviour
{
    public float MoveSpeed = 2.0f;
    public float RotSpeed = 360.0f;
    public float AttackRange = 1.0f;
    public float AttackDelay = 1.0f;
    protected float playTime = 0.0f;
    public float AttackPoint = 35.0f;


    public float MaxHP = 100.0f;
    public float _curHP = -100.0f;

    protected float curHP
    {
        get
        {
            if (_curHP < 0.0f) _curHP = MaxHP;
            
            return _curHP;
        }
        set => _curHP = Mathf.Clamp(value, 0.0f, MaxHP);
    }

    //MonoBehaviour가 생성자 처리를 하기 때문에 기존 방식 사용 불가

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
