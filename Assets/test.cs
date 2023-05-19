using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public LayerMask _layerMasks;

    // Start is called before the first frame update
    void Start()
    {

        LayerMask ground = LayerMask.GetMask("Groud");

        if((_layerMasks & ground) !=0)
            Debug.Log("Included");
        else
            Debug.Log("Not Included");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
