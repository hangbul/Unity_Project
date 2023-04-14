using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : CharacterProperty2D
{
    public LayerMask crashmask;
    float battleStance = 2.0f;
    Coroutine coJump = null;
    public bool isAir = false;
    bool isDown = false;
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
        if (Input.GetKeyDown(KeyCode.Space) && !isAir)
        {
            //Using RigidBody 
            //myRigid2D.AddForce(Vector2.up * 300.0f);

            //using Trigonometric Function

            if (coJump != null) StopCoroutine(coJump);
            coJump = StartCoroutine(Jumping(1.0f, 3.0f));
        }

    }

    private void FixedUpdate()
    {
        AirCheck();
    }
    IEnumerator Jumping(float totalTime, float maxHeight)
    {
        isDown = false;
        float t = 0.01f;
        float orgY = transform.position.y;

        while (t <= totalTime)
        {
            if (t >= totalTime * 0.5f) isDown = true;
             t += Time.deltaTime;
            float h = Mathf.Sin(Mathf.PI * (t / totalTime)) * maxHeight;
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 0.1f, crashmask);
            if (isDown)
            {
                if (hit.collider != null)
                {
                    transform.position = hit.point;
                    yield break;
                    isAir = true;
                }
                else
                {
                    isAir = false;
                    transform.position += Vector3.down * 9.8f * Time.deltaTime;
                }
            }

            transform.position = new Vector3(transform.position.x, orgY, transform.position.z) + Vector3.up * h;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, orgY, transform.position.z);
    }
    void AirCheck()
    {
        Vector2 orgPos = transform.position + Vector3.up * 0.05f;
        Vector2 dir = Vector2.down;
        ContactFilter2D filter = new ContactFilter2D();
        RaycastHit2D hit = Physics2D.Raycast(orgPos, dir, 0.1f, crashmask);
        if (hit.collider != null)
        {
            if (isDown)
            {
                //애니메이션
                if (coJump != null) StopCoroutine(coJump);
                transform.position = hit.point;
            }
            isAir = true;
        }
        else
        {
            isAir = false;
            transform.position += Vector3.down * 9.8f * Time.deltaTime;
        }
    }
    IEnumerator BattleStance()
    {
        myAnim.SetBool("BattleStance", true);
        yield return new WaitForSeconds(3.0f);
        myAnim.SetBool("BattleStance", false);

    }
}
