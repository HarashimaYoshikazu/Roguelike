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

    /// <summary>PlayerControllerクラス</summary>
    PlayerController _playerCon = null;
    public PlayerController PlayerCon => _playerCon;
    public void SetPlayerCon(PlayerController player) { _playerCon = player; }

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
        _UIManager.UpdateHPSlider(_hp);
        Debug.Log($"現在のHP：{_hp}");
        if(_hp<=0)
        {
            _gameCycle.GoResult();
        }
    }

    /// <summary>
    /// リトライ時にPlayerや敵をリセットする関数
    /// </summary>
    public void Reset()
    {
        _UIManager.SetExpMaxValue(GameData.ExpTable[_level]);
        _hp = _player.InitHP;
        _UIManager.UpdateHPSlider(_hp);
        _player.InitPos();
        _player.ResetSkill();
        _player.AddSkill(1);
        _enemyManager.ResetAllEnemy();
        _playerCon.ResetSpeed();
    }

    public void GetExp(int value)
    {
        _exp += value;
        _UIManager.UpdateExpSlider(_exp);
        if(GameData.ExpTable.Count >_level && GameData.ExpTable[_level]< _exp)
        {
            _level++;
            _gameCycle.StateMachine.Dispatch((int)StateEvent.LevelUp);
            _UIManager.SetExpMaxValue(GameData.ExpTable[_level]);
        }
    }

    /// <summary>
    /// レベルアップパネルのボタンが押下されたときによばれる関数
    /// </summary>
    /// <param name="table"></param>
    public void LevelUpSelect(SkillInfo table)
    {
        Debug.Log($"{table.Name}がレベルアップ");
        _player.AddSkill(table.TypeID);
    }

    public void IsPause(bool ispause, string pausetext)
    {
        _isPauseFlag = ispause;
        
        _UIManager?.SetActiveText(ispause,pausetext);
    }
}
