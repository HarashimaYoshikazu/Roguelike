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
        var alraedyHaving = _skillList.Where(s => s.SkillId == (SkillDef)skillId); //�����Ŏ����SkillID�ƍ��v����X�L���𒊏o
        if (alraedyHaving.Count() > 0)
        {
            alraedyHaving.Single().LevelUp(); //���o�����X�L���͈�������肦�Ȃ�����Single�֐����g�p�����x���A�b�v
        }
        else
        {
            ISkill newSkill = null; 

            switch ((SkillDef)skillId)�@//���߂ẴX�L����new���ă��X�g�ɒǉ�
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
