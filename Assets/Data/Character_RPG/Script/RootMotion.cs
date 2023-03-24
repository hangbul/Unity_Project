using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    public Animator myAnim = null;
    public Transform Root;

    void Start()
    {
    }

    private void OnAnimatorMove()
    {
        Root.position += myAnim.deltaPosition;
        Root.rotation *= myAnim.deltaRotation;
    }
}
