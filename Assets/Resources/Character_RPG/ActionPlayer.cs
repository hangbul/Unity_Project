using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayer : CharacterMovement
{
    Vector2 desireDirection;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputDiretion = Vector2.zero;
        inputDiretion.x = Input.GetAxisRaw("Horizontal");
        inputDiretion.y = Input.GetAxisRaw("Vertical");

        desireDirection = Vector2.Lerp(desireDirection, inputDiretion, Time.deltaTime * 1.5f); 

        myAnim.SetFloat("X", desireDirection.x);
        myAnim.SetFloat("Y", desireDirection.y);
        if (Input.GetMouseButtonDown(0))
        {
            myAnim.SetTrigger("Attack");
        }
    }
    int clickCheck = 0;
    Coroutine coCheck = null;
    public void ComboCheckStart()
    {
        coCheck = StartCoroutine(ComboChecking());
    }
    public void ComboCheckEnd()
    {
        myAnim.SetBool("isFailedCombo", false);

        StopCoroutine(coCheck);
        if(clickCheck == 0)
        {
            myAnim.SetTrigger("FaildCombo");
            myAnim.SetBool("isFailedCombo", true);
        }
    }

    IEnumerator ComboChecking()
    {
        clickCheck = 0;
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
                clickCheck++;
            yield return null;
        }
    }
}
