using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public Player Player => _player;
    /// <summary>Playerの初期化</summary>
    /// <param name="player">代入されるインスタンス</param>
    public void SetPlayer(Player player) { _player = player; _hp = player.InitHP; }

    /// <summary>UIManagerクラス</summary>
    UIManager _UIManager = null;
    public UIManager UIManager => _UIManager;
    public void SetUIManager(UIManager uIManager) { _UIManager = uIManager; }

    /// <summary>獲得したパッシブリスト</summary>
    List<int> _passive = new List<int>();

    /// <summary>ポーズ判定フラグ</summary>
    bool _isPauseFlag = false;
    public bool IsPauseFlag => _isPauseFlag;

    /// <summary>スキルボタンコントローラークラス</summary>
    SkillButton _skillButton = null;
    public void SetSkillButton(SkillButton skillButton) { _skillButton = skillButton; }

    public void SetUP()
    {
        _skillButton = GameObject.FindObjectOfType<SkillButton>();
    }

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
                Debug.Log($"{table.Name}がレベルアップ");
                //TODOスキルのレベル上げる
                break;

            case SelectType.Passive:
                _passive.Add(table.TypeID);
                break;

            case SelectType.Execute:

                break;
        }
    }

    public void IsPause(bool ispause)
    {
        _isPauseFlag = ispause;
        _UIManager?.SetActiveText(ispause);
    }

    //public void Pause()
    //{
    //    PauseAction?.Invoke();
    //}
    //public void Resume()
    //{
    //    ResumeAction?.Invoke();
    //}
}
