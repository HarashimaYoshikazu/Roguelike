using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    int _hp = 0;
    int _exp = 0;
    int _level = 0;

    Player _player = null;
    public void SetPlayer(Player player) { _player = player; _hp = player.InitHP; }

    List<int> _passive = new List<int>();

    public void ChangeHP(int value)
    {
        Mathf.Clamp(_hp,0, _player.InitHP);
        _hp += value;
        if(_hp<=0)
        {
            //TODOŽ€–Sˆ—
        }
    }

    public void GetExp(int value)
    {
        _exp += value;
        if(GameData.ExpTable.Count >_level && GameData.ExpTable[_level]< _exp)
        {
            _level++;
        }
    }

    public void LevelUpSelect(SkillInfo table)
    {
        switch (table.Type)
        {
            case SelectType.Skill:
                //TODOƒXƒLƒ‹‚ÌƒŒƒxƒ‹ã‚°‚é
                break;

            case SelectType.Passive:
                _passive.Add(table.TypeID);
                break;

            case SelectType.Execute:

                break;
        }
    }
}
