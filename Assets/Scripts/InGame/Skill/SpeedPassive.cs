using UnityEngine;

public class SpeedPassive : ISkill
{
    public SkillDef SkillId => SkillDef.SpeedUp;

    float _addSpeed = 0.5f;

    public void LevelUp()
    {
        GameManager.Instance.PlayerCon.ChangeSpeed(_addSpeed);
    }

    public void SetUp()
    {
        GameManager.Instance.PlayerCon.ChangeSpeed(_addSpeed);
    }

    public void Update()
    {
        
    }

    public void Reset()
    {
        
    }
}
