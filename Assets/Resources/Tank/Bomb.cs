using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool isFire = false, isQuitting = false;
    public List<GameObject> target;
    public GameObject _effect;
    float movedDistance;
    public float life_time = 5.0f;
    float speed = 10.0f;
    public LayerMask layermask;

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
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
            float delta = speed * Time.deltaTime;
            movedDistance += delta;
            if (movedDistance > 1000.0f)
            {
                //Destroy(this.gameObject);
                ObjectPool.Inst.ReleaseObject(this.gameObject);
                isFire = false;
            }
            Ray ray = new Ray();

            ray.origin = transform.position;
            ray.direction = transform.forward;
            if( Physics.Raycast(ray, out RaycastHit hit, delta, layermask))
            {
                DestroyObject(hit.transform.gameObject);
            }
                
            this.transform.Translate(Vector3.forward * delta);
            life_time -= Time.deltaTime;
        }

        
    }
    private void OnDestroy() //이벤트 함수 중 1 : 파괴될 때 실행
    {
        if (!isQuitting)
        {
            GameObject obj = Instantiate(_effect, this.transform.position, Quaternion.identity); return;
        }
    }

    public void OnFire()
    {
        isFire = true;
        this.transform.SetParent(null);
        this.GetComponent<Collider>().isTrigger = false;
    }

    void DestroyObject(GameObject obj)
    {
        if (obj.tag == "Bomb" || obj.tag == "Player") return;

        Destroy(obj);
        StartCoroutine(CreateTarget());
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("CollisionEnter");
        DestroyObject(other.gameObject);

    }
    IEnumerator CreateTarget()
    {
        this.GetComponent<Collider>().isTrigger = true;
        yield return new WaitForSeconds(0.1f);
        

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
