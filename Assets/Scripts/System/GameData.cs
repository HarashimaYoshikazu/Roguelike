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
    //ToDo GSSから値を読み込む
    static private List<int> _expTable = new List<int>()
    {
        1,5,10,30,70,190
    }
    ;
    /// <summary>
    /// レベルアップテーブルの読み取りプロパティ
    /// </summary>
    static public List<int> ExpTable => _expTable;
}
