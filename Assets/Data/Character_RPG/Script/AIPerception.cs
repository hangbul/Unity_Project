using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPerception : MonoBehaviour
{
    public LayerMask enemyMask;
    public List<Transform> myEnemylist = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if ((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            myEnemylist.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            if (myEnemylist.Contains(other.transform))
            {
                myEnemylist.Remove(other.transform);
            }
        }
    }
}
