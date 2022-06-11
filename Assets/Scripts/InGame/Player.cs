using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    [SerializeField]
    int _initHP = 40;
    public int InitHP => _initHP;

    List<ISkill> _skillList = new List<ISkill>();


    private void Awake()
    {
        GameManager.Instance.SetPlayer(this);
    }

    private void Update()
    {
        if(!GameManager.Instance.IsPauseFlag)
        {
            foreach (var skill in _skillList)
            {
                skill.Update();
            }
        }
    }

    public void AddSkill(int skillId)
    {
        var alraedyHaving = _skillList.Where(s => s.SkillId == (SkillDef)skillId); //引数で取ったSkillIDと合致するスキルを抽出
        if (alraedyHaving.Count() > 0)
        {
            alraedyHaving.Single().LevelUp(); //抽出したスキルは一つしかありえないためSingle関数を使用しレベルアップ
        }
        else
        {
            ISkill newSkill = null; 

            switch ((SkillDef)skillId)　//初めてのスキルはnewしてリストに追加
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
