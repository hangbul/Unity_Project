using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool isFire = false;
    private List<GameObject> target;

    public float life_time = 5.0f;
    float speed = 10.0f;

    void Start()
    {
        this.isFire = false;
        target = new List<GameObject>();
        // target.Add((GameObject)Resources.Load("Target")); //무조건 형변환
        target.Add(Resources.Load("Target") as GameObject);    //가능하면 형변환
        target.Add(Resources.Load("Target_Circle") as GameObject);    //가능하면 형변환
        target.Add(Resources.Load("Target_Sphere") as GameObject);    //가능하면 형변환

    }
    // 유니티 전체 중 안에 Resources 란 동일 명칭의 폴더가 root에 있는 Resources로 뭉쳐진다.
    // 뿐만 아니라 빌드 시 미사용되는 파일도 사용될 수 도 있다.
    

    // 엔진 업글 시 앞으로 사용하지 않는 함수가 있음

    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            life_time -= Time.deltaTime;
            if (life_time < 0)
                Destroy(this.gameObject);
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
        
        Vector3 tmp = other.transform.position;

        Destroy(other.gameObject);
        tmp.x = Random.Range(-8.0f, 8.0f);

        Vector3 _randPoint = tmp;
        GameObject obj = Instantiate(target[Random.Range(0, target.Count)]);
        

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
