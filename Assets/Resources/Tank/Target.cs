using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public TargetManager tm = null;
    bool isQuitting = false;

    private void Awake()
    {
        tm = GameObject.Find("TargetManager").GetComponent<TargetManager>();
        tm.addTarget();
    }
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    private void OnDestroy()
    {
        if (!isQuitting)
            tm.degreeTarget();
    }

}
