using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_Player_Property : MonoBehaviour
{
    public float Speed;
    public float Accel;
    public float RotSpeed;

    Animator _anim;

    protected Animator _MyAnim
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
