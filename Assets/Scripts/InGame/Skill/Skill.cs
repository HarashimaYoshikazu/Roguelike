interface ISkill
{
    SkillDef SkillId { get; }
    void SetUp();
    void Update();
    void LevelUp();
    void Reset();
}
//TODO�@�ЂƂ܂�Enum���g�������̕��@��͍�������
public enum SkillDef
{
    Invalid = 0,
    NetAttack = 1,
    Bullet = 2,
    SpeedUp = 3,
    Heal = 4
}


