using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObejct/Item", order = -1)]
public class ItemDataObject : ScriptableObject
{
    public enum GRADE{ Normal, Magic, Unique, Rare, Legend, Epic, Myth}

    [SerializeField] SkillDataObejct skill;

    [SerializeField] string _name;
    public string Name 
    {
        get => _name; 
    }

    [SerializeField] public float[] Power
    {
        get; private set;
    }
    public float GetPower(int lv, GRADE grade)
    {
        float temp = Power[lv];

        switch (grade)
        {
            case GRADE.Normal:
                break;
            case GRADE.Magic:
                temp *= 1.1f;
                break;
            case GRADE.Unique:
                temp *= 1.2f;
                break;
            case GRADE.Rare:
                temp *= 1.3f;
                break;
            case GRADE.Legend:
                temp *= 1.4f;
                break;
            case GRADE.Epic:
                temp *= 1.5f;
                break;
            case GRADE.Myth:
                temp *= 1.6f;
                break;
        }
        return temp;
    }
    [field: SerializeField]
    public int Price
    {
        get; private set;
    }

    [field: SerializeField]
    public GRADE Grade
    {
        get; private set;
    }

    

}
