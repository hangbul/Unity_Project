using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mon_Player : My_Player_Movement
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _MyAnim.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _MyAnim.SetTrigger("Skill");
        }
    }

    public void OnMove(Vector3 pos)
    {
        MoveToPos(pos);
    }
}
