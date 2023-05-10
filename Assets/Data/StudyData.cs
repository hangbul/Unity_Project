using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ITEM
{
    [SerializeField]
    //ItemDataObject Data;
    int Data;
    public ItemDataObject.GRADE grade;
    [SerializeField] int Level;
    public void Upgrade()
    {
        //if (Level == Data.Power.Length) return;
        if(UnityEngine.Random.Range(0,100) > 90)
        {
            Level++;
        }
    }
}
[Serializable]
public struct PlayerData
{
    public string ID;
    public string Gold;
}
public class StudyData : MonoBehaviour
{
    public ItemDataObject[] orgDatas;
    public PlayerData myData;
    public PlayerData myLoadData;
    public ITEM[] myDatas;
    public ITEM[] myLoadDatas;

    public string myjson;

    public ITEM[] itemList;
    string myText;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            FileManager.SaveText($"{Application.dataPath}/test.txt", "Test");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            myText = FileManager.LoadText($"{Application.dataPath}/test.txt");
            Debug.Log($"{myText}");
        }
        //Binary Save
        if (Input.GetKeyDown(KeyCode.F3))
        {
            FileManager.SaveBinary<PlayerData>($"{Application.dataPath}/test.txt", myData);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            myLoadData = FileManager.LoadBinary<PlayerData>($"{Application.dataPath}/test.txt");
            Debug.Log($"{myLoadData}");
        }
        //Array Binary Save
        if (Input.GetKeyDown(KeyCode.F5))
        {
            FileManager.SaveBinaryArray<ITEM>($"{Application.dataPath}/test.txt", myDatas);
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            myLoadDatas = FileManager.LoadBinaryArray<ITEM>($"{Application.dataPath}/test.txt");
            Debug.Log($"{myLoadData}");
        }
        //Json Save
        if (Input.GetKeyDown(KeyCode.F7))
        {
            myjson = JsonUtility.ToJson(myData);
            FileManager.SaveText($"{Application.dataPath}/test.txt", myjson);

        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            myText = FileManager.LoadText($"{Application.dataPath}/test.txt");
            myLoadData = JsonUtility.FromJson<PlayerData>(myText);

        }
    }
}
