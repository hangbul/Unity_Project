using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseErath : MonoBehaviour
{
    public static DefenseErath _Instance = null;
    //�ڽ��� �ν��Ͻ� ���� = ������ ������ ���� = �ڽ� this�� ���� -> ��ΰ� this�� ����

    void Start()
    {
        _Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
