using System;
using System.Collections.Generic;

/// <summary>
/// �X�L���̎�ށA��ނɂ���Č�̏�����ύX����
/// </summary>
public enum SelectType
{
    Skill = 1,
    Passive = 2,
    Execute = 3,
}
[Serializable]
public class SkillInfo
{
    public SelectType Type;
    public int TypeID;
    public string Name;
    public int Level;
    public int Probability;
}


/// <summary>
/// �}�X�^�[�f�[�^
/// </summary>
public class GameData
{
    static public List<SkillInfo> SkillSelectTable = new List<SkillInfo>()
    {
        new SkillInfo(){Type =SelectType.Skill,TypeID =1,Name ="�͂��݂u���X",Level=0,Probability =50},
        new SkillInfo(){Type =SelectType.Skill,TypeID =2,Name ="�ԕ��e",Level=0,Probability =50},
        new SkillInfo(){Type =SelectType.Skill,TypeID =3,Name ="�X�s�[�hUP",Level=0,Probability =50},
        new SkillInfo(){Type =SelectType.Skill,TypeID =4,Name ="��",Level=0,Probability =50},
    };

    //ToDo GSS����l��ǂݍ���
    static private List<int> _expTable = new List<int>()
    {
        5,10,20,30,40,50,60,70,80,90,100
    }
    ;
    /// <summary>
    /// ���x���A�b�v�e�[�u���̓ǂݎ��v���p�e�B
    /// </summary>
    static public List<int> ExpTable => _expTable;
}
