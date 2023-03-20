using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool isFire = false;
    public GameObject[] target;

    float speed = 10.0f;

    void Start()
    {
        this.isFire = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void OnFire()
    {
        isFire = true;
        this.transform.SetParent(null);
        this.GetComponent<Collider>().isTrigger = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("CollisionEnter");
        if (other.gameObject.tag == "Bomb") return;
        
        Destroy(other.gameObject);
        Vector3 _randPoint = new Vector3(Random.Range(-16, 16), 5, Random.Range(-16, 16));
        GameObject obj = Instantiate(target[Random.Range(0, target.Length)]);
            
        
        Destroy(this.gameObject);


    }
    private void OnCollisionStay(Collision other)
    {
        Debug.Log("CollisionStay");
    }
    private void OnCollisionExit(Collision other)
    {
        Debug.Log("CollisionExit");
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriigerEnter");
        
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("TriigerStay");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("TriigerExit");
    }

}
