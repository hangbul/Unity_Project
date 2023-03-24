using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Event 사용시 미사용
//public delegate void ClickAct(Vector3 pos);

public class Picking : MonoBehaviour
{
    public LayerMask pickMask;
    public UnityEvent<Vector3> clickAction = null;
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
            if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask))
            {
                clickAction?.Invoke(hit.point);
            }
        }

    }

}
