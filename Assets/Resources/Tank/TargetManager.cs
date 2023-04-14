using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{

    public GameObject[] targets;

    public int target_count = 0;
    public int target_limit = 5;

    private void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    IEnumerator SpawnTarget()
    {
        while (true)
        {
            if (target_count < target_limit)
            {
                Vector3 tmp;
                tmp.x = Random.Range(-8.0f, 8.0f);
                tmp.y = 0.5f;
                tmp.z = Random.Range(-8.0f, 8.0f);
                GameObject obj = Instantiate(targets[Random.Range(0, targets.Length)]);
                obj.transform.position = tmp;
            }
            yield return new WaitForSeconds(5.0f);
        }
    }


    public void addTarget()
    {
        target_count++;
    }

    public void degreeTarget()
    {
        target_count--;
    }
}
