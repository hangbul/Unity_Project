using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public LayerMask enemyMask;

    public GameObject[] Ori_Asteroid;

    public float spawn_Max_Distance = 20.0f;
    public float spawn_Min_Distance = 15.0f;
    public float spawn_Distance = 20.0f;
    public float spawn_Turm_sec = 2.0f;    

    private float circle_num = 400.0f;
    public Camera _main_cam;
    void Start()
    {
        StartCoroutine(Spawn_Asteroid());
    }

    void Update()
    {
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



    }

    IEnumerator Spawn_Asteroid()
    {
        while (Earth.Instance != null)
        {
            int rnd = Random.Range(0, 3);

            //case 01
            {
                /*
                Vector3 pos = Vector3.zero;
                while (Mathf.Approximately(pos.magnitude, 0.0f))
                {
                    pos.x = Random.Range(-1.0f, 1.0f);
                    pos.z = Random.Range(-1.0f, 1.0f);
                }
                Vector3 rnddir = (pos - Earth.Instance._myEarth.position).normalized;
                pos = Earth.Instance._myEarth.position + rnddir * spawn_Distance;
                GameObject obj = Instantiate(Ori_Asteroid[rnd], pos, Quaternion.identity);
                 */
            }
            //case 02
            {
                Vector3 pos = new Vector3(Mathf.Cos(Mathf.PI * Time.deltaTime * circle_num), 0, Mathf.Sin(Mathf.PI * Time.deltaTime * circle_num)) * spawn_Distance;
                GameObject obj = Instantiate(Ori_Asteroid[rnd], pos, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawn_Turm_sec);
        }
    }

     
}
