using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSkill : ISkill
{
    public SkillDef SkillId => SkillDef.Bullet;

    float _timer = 0f;
    float _interval = 1f;
    int _bulletValue = 1;

    
    public void LevelUp()
    {

    }

    public void SetUp()
    {

    }

    public void Update()
    {
        _timer += Time.deltaTime;

        if (_timer> _interval)
        {
            var enemyList = GameManager.Instance.EnemyManager.EnemyPool.ObjectList;
            EnemyController[] targets = new EnemyController[_bulletValue];

            float prevMinDis = -1;
            foreach (var i in enemyList)
            {
                Vector3 vec = i.transform.position - GameManager.Instance.Player.transform.position;
                if (prevMinDis == -1 || vec.sqrMagnitude < prevMinDis)
                {
                    for (int k = 1; k < _bulletValue; k++)
                    {
                        targets[k] = targets[k - 1];
                    }
                    targets[0] = i;
                    prevMinDis = vec.sqrMagnitude;
                }
            }



            _timer = 0;
        }
    }
}