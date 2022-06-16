using System;
using System.Collections.Generic;

/// <summary>
/// スキルの種類、種類によって後の処理を変更する
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
/// マスターデータ
/// </summary>
public class GameData
{
    static public List<SkillInfo> SkillSelectTable = new List<SkillInfo>()
    {
        new SkillInfo(){Type =SelectType.Skill,TypeID =1,Name ="はちみつブレス",Level=0,Probability =50},
        new SkillInfo(){Type =SelectType.Skill,TypeID =2,Name ="花粉弾",Level=0,Probability =50},
        new SkillInfo(){Type =SelectType.Skill,TypeID =3,Name ="スピードUP",Level=0,Probability =50},
        new SkillInfo(){Type =SelectType.Skill,TypeID =4,Name ="回復",Level=0,Probability =50},
    };

    //ToDo GSSから値を読み込む
    static private List<int> _expTable = new List<int>()
    {
        5,10,20,30,40,50,60,70,80,90,100
    }
    ;
    /// <summary>
    /// レベルアップテーブルの読み取りプロパティ
    /// </summary>
    static public List<int> ExpTable => _expTable;
}
