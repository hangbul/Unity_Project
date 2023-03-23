using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseErath : MonoBehaviour
{
    public static DefenseErath _Instance = null;
    //자신을 인스턴스 생성 = 데이터 영역에 생성 = 자신 this를 참조 -> 모두가 this를 참조

    void Start()
    {
        _Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
