
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : Singleton<ObjectPool<T>>
{
    Dictionary<string, Queue<T>> myPool = new Dictionary<string, Queue<T>>();

    public GameObject GetObject<T>(GameObject org, Vector3 pos, Quaternion rot = default, Transform parent = null)
    {
        if(myPool.ContainsKey(org.name))
        {
            if (myPool[org.name].Count > 0)
            {
                GameObject obj = myPool[org.name].Dequeue() as GameObject;
                obj.SetActive(true);
                obj.transform.SetParent(parent);
                obj.transform.position = pos;
                obj.transform.rotation = rot;
                return obj;
            }
        }
        else
        {
            //myPool[org.name] = new Queue<GameObject>();
        }
        return Instantiate(org, pos, rot, parent);

    }

    public void ReleaseObject<T>(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.SetActive(false);
        //myPool[obj.name].Enqueue(obj);
    }
}
