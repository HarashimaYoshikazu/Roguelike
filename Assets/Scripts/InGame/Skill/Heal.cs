using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : ISkill
{
    public SkillDef SkillId => SkillDef.Heal;

    public void LevelUp()
    {
        GameManager.Instance.ChangeHP(5);
    }

    public void Reset()
    {
        
    }

    public void SetUp()
    {
        GameManager.Instance.ChangeHP(5);
    }

    public void Update()
    {
        
    }
}
