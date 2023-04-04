using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : CharacterProperty2D
{
    float battleStance = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!myAnim.GetBool("isAttacking"))
        {
            Vector2 dir = Vector2.zero;
            dir.x = Input.GetAxis("Horizontal");
            transform.Translate(dir * MoveSpeed * Time.deltaTime);
            if (!Mathf.Approximately(dir.x, 0.0f))
            {
                myAnim.SetBool("IsMoving", true);
                if (dir.x < 0.0f)
                    myRenderer.flipX = false;
                else
                    myRenderer.flipX = true;
            }
            else
                myAnim.SetBool("IsMoving", false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            myAnim.SetTrigger("Attack");
            StopAllCoroutines();
            StartCoroutine(BattleStance());
        }

    }

    IEnumerator BattleStance()
    {
        myAnim.SetBool("BattleStance", true);
        yield return new WaitForSeconds(3.0f);
        myAnim.SetBool("BattleStance", false);

    }
}
