using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    int _initHP = 40;
    public int InitHP => _initHP;

    //List<>

    //public void AddSkill(int skillId)
    //{
    //    var having = _skill.Where(s => s.SkillId == (SkillDef)skillId);
    //    if (having.Count() > 0)
    //    {
    //        having.Single().Levelup();
    //    }
    //    else
    //    {
    //        ISkill newSkill = null;
    //        switch ((SkillDef)skillId)
    //        {
    //            case SkillDef.ShotBullet:
    //                newSkill = new ShotBullet();
    //                break;

    //            case SkillDef.AreaAttack:
    //                newSkill = new AreaAttack();
    //                break;
    //        }

    //        if (newSkill != null)
    //        {
    //            newSkill.Setup();
    //            _skill.Add(newSkill);
    //        }
    //    }
    //}
}
