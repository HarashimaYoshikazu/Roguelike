using UnityEngine;
public class NetSkill : ISkill
{
    public SkillDef SkillId => SkillDef.NetAttack;

    float _timer = 0f;
    float _interval = 3f;
    bool[] _weaponActive = new bool[4];
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
                if (n.EffectTime+0.5f < _interval)
                {
                    n.ChangeEffectTime(0.5f);
                }
                
            }            
        }
    }

    public void SetUp()
    {
        _weaponActive = new bool[GameManager.Instance.Player.Weapons.Length];
        _weaponActive[0] = true;
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
            GameManager.Instance.Player.AudioPlay(1);
            _timer = 0f;
        }
    }

    public void Reset()
    {
        for(int i = 0;i<_weaponActive.Length;i++)
        {
            _weaponActive[i] = false;
        }
    }
}
