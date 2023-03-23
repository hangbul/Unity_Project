using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public LayerMask crashMask;
    
    public static Earth Instance = null;
    public float rotSpeed = 10.0f;

    public Transform _myEarth = null;
    public GameObject _effect = null;
    
    private bool isQuitting = false;

    private void Awake()
    {
        Instance = this;
        _myEarth = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * rotSpeed);
    }
    
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    
    private void OnDestroy() //이벤트 함수 중 1 : 파괴될 때 실행
    {
        if (!isQuitting)
        {
            
            return;
        }
    }
    void OnTriggerEnter (Collider other)
    {
        if (((1 << other.gameObject.layer) & crashMask) != 0) 
        {
            GameObject obj = Instantiate(_effect, this.transform.position, Quaternion.identity);
            obj.transform.SetParent(null);

            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
