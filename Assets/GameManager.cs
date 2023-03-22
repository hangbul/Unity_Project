using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool Spawning = true;
    public LayerMask enemyMask;

    public GameObject[] Ori_Asteroid;
    GameObject Earth;

    public float spawn_Max_Distance = 20.0f;
    public float spawn_Min_Distance = 15.0f;
    public float spawn_Distance = 20.0f;
    public float spawn_Turm_sec = 2.0f;    

    private float circle_num = 400.0f;
    public Camera _main_cam;
    void Start()
    {
        Earth = GameObject.Find("Earth");
        StartCoroutine(Spawn_Asteroid());
    }

    void Update()
    {
        spawn_Distance = Mathf.Clamp(spawn_Distance * Mathf.Sin(Time.deltaTime), spawn_Min_Distance, spawn_Max_Distance);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _main_cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit)){

                if (((1 << hit.transform.gameObject.layer) & enemyMask) == 0)
                {
                    Destroy(hit.transform.gameObject);
                }
                Debug.Log(hit.transform.gameObject.layer);
            }

        }

        if (Earth == null && Spawning)
        {
            Spawning = false;
        }



    }

    IEnumerator Spawn_Asteroid()
    {
        while (Spawning)
        {
            int rnd = Random.Range(0, 3);
            

            Vector3 _point = new Vector3(Mathf.Cos(Mathf.PI * Time.deltaTime * circle_num) , 0, Mathf.Sin(Mathf.PI * Time.deltaTime * circle_num) ) * spawn_Distance;
            GameObject obj = Instantiate(Ori_Asteroid[rnd]);
            obj.transform.position = _point;

            yield return new WaitForSeconds(spawn_Turm_sec);
        }
    }

     
}
