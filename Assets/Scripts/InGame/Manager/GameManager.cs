using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    /// <summary>Player�ϐ�</summary>
    Player _player = null;
    /// <summary>Player�̏�����</summary>
    /// <param name="player">��������C���X�^���X</param>
    public void SetPlayer(Player player) { _player = player; _hp = player.InitHP; }

    /// <summary>�l�������p�b�V�u���X�g</summary>
    List<int> _passive = new List<int>();

    /// <summary>�X�L���{�^���R���g���[���[�N���X</summary>
    SkillButton _skillButton = null;

    public void ChangeHP(int value)
    {
        Mathf.Clamp(_hp,0, _player.InitHP);
        _hp += value;
        if(_hp<=0)
        {
            //TODO���S����
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
    /// ���x���A�b�v�p�l���̃{�^�����������ꂽ�Ƃ��ɂ�΂��֐�
    /// </summary>
    /// <param name="table"></param>
    public void LevelUpSelect(SkillInfo table)
    {
        switch (table.Type)
        {
            case SelectType.Skill:
                //TODO�X�L���̃��x���グ��
                break;

            case SelectType.Passive:
                _passive.Add(table.TypeID);
                break;

            case SelectType.Execute:

                break;
        }
    }
}
