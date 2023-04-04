using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Event 사용시 미사용
//public delegate void ClickAct(Vector3 pos);

public class Picking : MonoBehaviour
{
    public LayerMask pickMask;
    public LayerMask enemyMask;
    public UnityEvent<Vector3> LeftClickAction = null;
    public UnityEvent<Vector3> RightClickAction = null;

    public UnityEvent<Transform> attackAction = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask | enemyMask))
            {
                if((1 << hit.transform.gameObject.layer & enemyMask )!=0)
                {
                    attackAction?.Invoke(hit.transform);
                }

                else LeftClickAction?.Invoke(hit.point);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask ))
            {
                RightClickAction?.Invoke(hit.point);
            }
        }
    }

}
