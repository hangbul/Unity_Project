using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavPlayer : CharacterProperty
{
    private NavMeshAgent myNav;

    // Start is called before the first frame update
    void Start()
    {
        myNav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetFloat("Speed", myNav.velocity.magnitude / myNav.speed);
    }

    public void OnMove(Vector3 pos)
    {
        StopAllCoroutines();
        StartCoroutine(OnJumpJumpableMoving(pos));

        //myNav.SetDestination(pos);
    }

    public void OnWarp(Vector3 pos)
    {
        myNav.Warp(pos);
    }



    IEnumerator Moving(Vector3 pos)
    {
        myAnim.SetBool("isMoving", true);
        while (myNav.pathPending || myNav.remainingDistance > myNav.stoppingDistance)
        {
            yield return null;
        }
        myAnim.SetBool("isMoving", false);


    }
    IEnumerator OnJumpJumpableMoving(Vector3 pos)
    {
        myNav.SetDestination(pos);
        while (myNav.pathPending || myNav.remainingDistance > myNav.stoppingDistance)
        {
            if (myNav.isOnOffMeshLink)
            {
                myAnim.SetBool("isAir", true);
                myNav.isStopped = true;
                Vector3 endPos = myNav.currentOffMeshLinkData.endPos;
                Vector3 dir = endPos - transform.position;
                float dist = dir.magnitude;
                dir.Normalize();

                while(dist > 0.0f)
                {
                    float delta = myNav.speed * Time.deltaTime;
                    if(dist - delta < 0.0f)
                    {
                        delta = dist;
                    }
                    dist -= delta;
                    transform.Translate(dir * delta, Space.World);
                 
                    yield return null;
                }
                myAnim.SetBool("isAir", false);
                myNav.CompleteOffMeshLink();
                myNav.isStopped = false;
            }
            yield return null;

        }
    }
}
