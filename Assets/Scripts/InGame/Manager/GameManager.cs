using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private int _hp = 0;
    /// <summary>HP変数</summary>
    public int HP => _hp;

    private int _exp = 0;
    /// <summary>経験値変数</summary>
    public int Exp => _exp;

    private int _level = 1;
    /// <summary>レベル変数</summary>
    public int Level => _level;

    /// <summary>Player変数</summary>
    Player _player = null;
    /// <summary>Playerの初期化</summary>
    /// <param name="player">代入されるインスタンス</param>
    public void SetPlayer(Player player) { _player = player; _hp = player.InitHP; }

    /// <summary>獲得したパッシブリスト</summary>
    List<int> _passive = new List<int>();

    /// <summary>スキルボタンコントローラークラス</summary>
    SkillButton _skillButton = null;

    public void ChangeHP(int value)
    {
        Mathf.Clamp(_hp,0, _player.InitHP);
        _hp += value;
        if(_hp<=0)
        {
            //TODO死亡処理
        }
    }

    public void GetExp(int value)
    {
        _exp += value;
        if(GameData.ExpTable.Count >_level && GameData.ExpTable[_level]< _exp)
        {
            _level++;
            _skillButton.SelectStart();
        }
    }

    /// <summary>
    /// レベルアップパネルのボタンが押下されたときによばれる関数
    /// </summary>
    /// <param name="table"></param>
    public void LevelUpSelect(SkillInfo table)
    {
        switch (table.Type)
        {
            case SelectType.Skill:
                //TODOスキルのレベル上げる
                break;

            case SelectType.Passive:
                _passive.Add(table.TypeID);
                break;

            case SelectType.Execute:

                break;
        }
    }
}
