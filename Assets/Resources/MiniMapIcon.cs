using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniMapIcon : MonoBehaviour
{
    public Image myImage = null;
    public Transform myRoot = null;
    RectTransform parentRect;


    public void Initialize(Transform p, Color color)
    {
        myRoot = p;
        myImage.color = color;
    }
    // Start is called before the first frame update
    void Start()
    {
        parentRect = transform.parent.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.allCameras[1].WorldToViewportPoint(myRoot.position);
        pos.x = pos.x * parentRect.sizeDelta.x - parentRect.sizeDelta.x * 0.5f;
        pos.y = pos.y * parentRect.sizeDelta.y - parentRect.sizeDelta.y * 0.5f;
        transform.localPosition = pos;
    }
}
