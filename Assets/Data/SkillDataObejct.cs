using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillDataObejct", menuName = "ScriptableObejct/Skill", order = 0)]
public class SkillDataObejct : ScriptableObject
{
    public enum Type { Passive, Active}
    
    public Type myType;

    [field:SerializeField] public string Content
    {
        get; private set;
    }

    public int Code;
    public float Value1;
    public float Value2;


}
