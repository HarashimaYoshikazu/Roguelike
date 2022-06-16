using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    private int _hp = 0;
    /// <summary>HP�ϐ�</summary>
    public int HP => _hp;

    private int _exp = 0;
    /// <summary>�o���l�ϐ�</summary>
    public int Exp => _exp;

    private int _level = 1;
    /// <summary>���x���ϐ�</summary>
    public int Level => _level;

    /// <summary>�l�������p�b�V�u���X�g</summary>
    List<int> _passive = new List<int>();

    /// <summary>�|�[�Y����t���O</summary>
    bool _isPauseFlag = false;
    public bool IsPauseFlag => _isPauseFlag;


    /// <summary>Player�N���X</summary>
    Player _player = null;
    public Player Player => _player;
    public void SetPlayer(Player player) { _player = player; _hp = player.InitHP; }

    /// <summary>PlayerController�N���X</summary>
    PlayerController _playerCon = null;
    public PlayerController PlayerCon => _playerCon;
    public void SetPlayerCon(PlayerController player) { _playerCon = player; }

    /// <summary>UIManager�N���X</summary>
    UIManager _UIManager = null;
    public UIManager UIManager => _UIManager;
    public void SetUIManager(UIManager uIManager) { _UIManager = uIManager; }

    /// <summary>�Q�[���T�C�N���N���X</summary>
    GameCycle _gameCycle = null;
    public GameCycle GameCycle => _gameCycle;
    public void SetGameCycle(GameCycle gameCycle) {_gameCycle = gameCycle; }

    /// <summary>�X�L���{�^���R���g���[���[�N���X</summary>
    SkillButton _skillButton = null;
    public SkillButton SkillButton => _skillButton;
    public void SetSkillButton(SkillButton skillButton) { _skillButton = skillButton; }

    /// <summary>�G�l�~�[�}�l�[�W���[�N���X</summary>
    EnemyManager _enemyManager = null;
    public EnemyManager EnemyManager => _enemyManager;
    public void SetEnemyManager(EnemyManager enemyma) { _enemyManager = enemyma; }

    public void ChangeHP(int value)
    {
        Mathf.Clamp(_hp,0, _player.InitHP);
        _hp += value;
        _UIManager.UpdateHPSlider(_hp);
        Debug.Log($"���݂�HP�F{_hp}");
        if(_hp<=0)
        {
            _gameCycle.GoResult();
        }
    }

    /// <summary>
    /// ���g���C����Player��G�����Z�b�g����֐�
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
    /// ���x���A�b�v�p�l���̃{�^�����������ꂽ�Ƃ��ɂ�΂��֐�
    /// </summary>
    /// <param name="table"></param>
    public void LevelUpSelect(SkillInfo table)
    {
        Debug.Log($"{table.Name}�����x���A�b�v");
        _player.AddSkill(table.TypeID);
    }

    public void IsPause(bool ispause, string pausetext)
    {
        _isPauseFlag = ispause;
        
        _UIManager?.SetActiveText(ispause,pausetext);
    }
}
