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

/// <summary>
/// �}�X�^�[�f�[�^
/// </summary>
public class GameData
{
    //ToDo GSS����l��ǂݍ���
    static private List<int> _expTable = new List<int>()
    {
        1,5,10,30,70,190
    }
    ;
    /// <summary>
    /// ���x���A�b�v�e�[�u���̓ǂݎ��v���p�e�B
    /// </summary>
    static public List<int> ExpTable => _expTable;
}
