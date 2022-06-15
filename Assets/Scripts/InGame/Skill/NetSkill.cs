using UnityEngine;
public class NetSkill : ISkill
{
    public SkillDef SkillId => SkillDef.NetAttack;

    float _timer = 0f;
    float _interval = 1f;
    bool[] _weaponActive = null;
    public void LevelUp()
    {
        bool hoge = false;

        for (int i = 0;i<_weaponActive.Length;i++)
        {
            if (!_weaponActive[i])
            {
                _weaponActive[i] = true;
                hoge = true;
                break;
            }
        }
        if (!hoge)
        {
            foreach (var n in GameManager.Instance.Player.Weapons)
            {
                n.ChangeDamage(1);
                n.ChangeEffectTime(1f);
            }            
        }
    }

    public void SetUp()
    {
        _weaponActive = new bool[GameManager.Instance.Player.Weapons.Length];
    }

    public void Update()
    {
        _timer+= Time.deltaTime;

        if(_timer > _interval)
        {
            for(int i=0;i< _weaponActive.Length;i++)
            {
                if (_weaponActive[i]) 
                {
                    GameManager.Instance.Player.Weapons[i].gameObject.SetActive(true);
                }

            }
            _timer = 0f;
        }
    }
}
