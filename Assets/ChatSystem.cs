using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatSystem : MonoBehaviour
{
    public Transform myContents;
    public TMPro.TMP_InputField myInput;
    public Scrollbar myScroll;
    public void AddMessage(string msg)
    {
        if(msg == string.Empty)
        {
            myInput.DeactivateInputField();
            return;
        }
        (Instantiate(Resources.Load("UI/ChatMessage"), myContents) as GameObject).GetComponent<ChatMessage>().SetMessage(msg);
        myInput.text = string.Empty;
        myInput.ActivateInputField();
        StartCoroutine(MakingZero());
    }

    IEnumerator MakingZero()
    {
        yield return new WaitForEndOfFrame();
        myScroll.value = 0.0f;
    }
}
