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

    /// <summary>獲得したパッシブリスト</summary>
    List<int> _passive = new List<int>();

    /// <summary>ポーズ判定フラグ</summary>
    bool _isPauseFlag = false;
    public bool IsPauseFlag => _isPauseFlag;



    /// <summary>Playerクラス</summary>
    Player _player = null;
    public Player Player => _player;
    public void SetPlayer(Player player) { _player = player; _hp = player.InitHP; }

    /// <summary>UIManagerクラス</summary>
    UIManager _UIManager = null;
    public UIManager UIManager => _UIManager;
    public void SetUIManager(UIManager uIManager) { _UIManager = uIManager; }

    /// <summary>ゲームサイクルクラス</summary>
    GameCycle _gameCycle = null;
    public GameCycle GameCycle => _gameCycle;
    public void SetGameCycle(GameCycle gameCycle) {_gameCycle = gameCycle; }

    /// <summary>スキルボタンコントローラークラス</summary>
    SkillButton _skillButton = null;
    public SkillButton SkillButton => _skillButton;
    public void SetSkillButton(SkillButton skillButton) { _skillButton = skillButton; }

    /// <summary>エネミーマネージャークラス</summary>
    EnemyManager _enemyManager = null;
    public EnemyManager EnemyManager => _enemyManager;
    public void SetEnemyManager(EnemyManager enemyma) { _enemyManager = enemyma; }

    public void ChangeHP(int value)
    {
        Mathf.Clamp(_hp,0, _player.InitHP);
        _hp += value;
        Debug.Log($"現在のHP：{_hp}");
        if(_hp<=0)
        {
            _gameCycle.StateMachine.Dispatch((int)StateEvent.GameOver);
        }
    }

    /// <summary>
    /// リトライ時にPlayerや敵をリセットする関数
    /// </summary>
    public void Reset()
    {
        _hp = _player.InitHP;
        _player.InitPos();
        _enemyManager.ResetAllEnemy();
    }

    public void GetExp(int value)
    {
        _exp += value;
        if(GameData.ExpTable.Count >_level && GameData.ExpTable[_level]< _exp)
        {
            _level++;
            _gameCycle.StateMachine.Dispatch((int)StateEvent.LevelUp);
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
                _player.AddSkill(table.TypeID);
                break;

            case SelectType.Passive:
                _passive.Add(table.TypeID);
                break;

            case SelectType.Execute:

                break;
        }
    }

    public void IsPause(bool ispause, string pausetext)
    {
        _isPauseFlag = ispause;
        
        _UIManager?.SetActiveText(ispause,pausetext);
    }
}
