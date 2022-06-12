interface ISkill
{
    SkillDef SkillId { get; }
    void SetUp();
    void Update();
    void LevelUp();
}
//TODO@‚Ğ‚Æ‚Ü‚¸Enum‚ğg‚¤‚ª‘¼‚Ì•û–@‚ğ–Íõ‚µ‚½‚¢
public enum SkillDef
{
    Invalid = 0,
    NetAttack =1,
    Bullet = 2
}

