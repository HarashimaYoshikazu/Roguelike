using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    int _initHP = 40;
    public int InitHP => _initHP;

    List<ISkill> _skillList = new List<ISkill>();

    public void AddSkill(int skillId)
    {
        var having = _skillList.Where(s => s.SkillId == (SkillDef)skillId);
        if (having.Count() > 0)
        {
            having.Single().LevelUp();
        }
        else
        {
            ISkill newSkill = null;

            switch ((SkillDef)skillId)
            {
                case SkillDef.NetAttack:
                    newSkill = new NetSkill();
                    break;
            }

            if (newSkill != null)
            {
                newSkill.SetUp();
                _skillList.Add(newSkill);
            }
        }
    }
}
