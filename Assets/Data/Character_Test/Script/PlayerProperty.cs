using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    public float RotSpeed = 360.0f;

    Animator _anim = null;

    protected Animator myAnim
    {
        get
        {
            _anim = GetComponent<Animator>();
            if(_anim == null)
            {
                _anim = GetComponentInChildren<Animator>();
            }
            return _anim;
        }

    }

}