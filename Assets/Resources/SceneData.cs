using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData: MonoBehaviour
{
    public static SceneData Inst = null;
    public Canvas screenCanvas = null;
    public Transform miniMap = null;

    private void Awake()
    {
        Inst = this;
    }

}
