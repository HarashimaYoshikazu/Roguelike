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

    private int _stackExp = 0;

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
        _hp=　Mathf.Clamp(_hp+value,0, _player.InitHP);
        _UIManager.UpdateHPSlider(_hp);
        Debug.Log($"現在のHP：{_hp}");
        if(_hp<=0)
        {
            _playerCon.IsDeathAnim(true);
            _gameCycle.GoResult();
        }
    }

    /// <summary>
    /// リトライ時にPlayerや敵をリセットする関数
    /// </summary>
    public void Reset()
    {      
        _hp = _player.InitHP;
        _exp = 0;
        _stackExp = 0;
        _UIManager.UpdateExpSlider(_exp);
        _UIManager.UpdateHPSlider(_hp);
        _player.InitPos();
        _player.ResetSkill();
        _player.AddSkill(2);
        _enemyManager.ResetAllEnemy();
        _playerCon.ResetSpeed();
        PlayerCon.IsDeathAnim(false);
        _level = 1;
        _UIManager.UpdateLevelUpText(_level);
        _UIManager.SetExpMaxValue(GameData.ExpTable[_level - 1]);

        //FIXME
        var objs = GameObject.FindObjectsOfType<HealItem>();
        foreach (var i in objs)
        {
            i.gameObject.SetActive(true);
        }
    }

    public void GetExp(int value)
    {
        _exp += value;
        _stackExp += value;
        _UIManager.UpdateExpSlider(_stackExp);
        if(GameData.ExpTable.Count >_level && GameData.ExpTable[_level-1]< _exp)
        {
            _stackExp = 0;
            _level++;
            _gameCycle.StateMachine.Dispatch((int)StateEvent.LevelUp);
            _UIManager.UpdateExpSlider(_stackExp);
            _UIManager.SetExpMaxValue(GameData.ExpTable[_level-1]);
            _UIManager.UpdateLevelUpText(_level);
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
