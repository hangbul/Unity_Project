using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Inst = null;
    private void Awake()
    {
        if (Inst != null)
        {
            if (Inst != this)
            {
                Destroy(this);
            }
        }
        else
        {
            Inst = this;
            DontDestroyOnLoad(this);
        }
    }

    public void ChangeScene(int i)
    {
        StartCoroutine(Loading(i));
         //�޸� �߿�( �߰� �ε� �� �ʿ� why : �� �ε��� ���� ���� ���� ���� �޸𸮰� �� ���� ���� ����� ��찡 �߻��ϱ� ����)

    }

    IEnumerator Loading(int i)
    {
        yield return SceneManager.LoadSceneAsync(3);
        AsyncOperation op = SceneManager.LoadSceneAsync(i);     //return AsyncOperation operation;
        op.allowSceneActivation = false;    //�� �ε��� ������ �ٷ� �ش� ���� �ٷ� Ȱ��ȭ = > �ε� ���� �ȵ�
        Slider loadingSlider = FindAnyObjectByType<Slider>();

        while (op.isDone)
        {
            loadingSlider.value = op.progress / 0.9f;  //(0~0.9)
            if (Mathf.Approximately(loadingSlider.value, 1.0f))
            {
                yield return new WaitForSeconds(1.0f);  //debug�� ������

                op.allowSceneActivation = true;
            }

            yield return new WaitForSeconds(0.5f);  //debug�� ������
        }

    }

}
