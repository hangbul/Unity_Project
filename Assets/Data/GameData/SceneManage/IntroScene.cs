using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Mv2NextScene());
    }


    IEnumerator Mv2NextScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(1);
    }
}
