interface ISkill
{
    SkillDef SkillId { get; }
    void SetUp();
    void Update();
    void LevelUp();
    void Reset();
}
//TODO　ひとまずEnumを使うが他の方法を模索したい
public enum SkillDef
{
    Invalid = 0,
    NetAttack = 1,
    Bullet = 2,
    SpeedUp = 3,
    Heal = 4
}


