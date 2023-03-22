using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

public class Earth : MonoBehaviour
{

    public float rotSpeed = 10.0f;
    public GameObject _effect = null;
    
    private bool isQuitting = false;
    
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
            GameObject obj = Instantiate(_effect, this.transform.position, Quaternion.identity); return;
        }
    }
    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") )
        {
            Destroy(this.gameObject);
        }
    }
}
