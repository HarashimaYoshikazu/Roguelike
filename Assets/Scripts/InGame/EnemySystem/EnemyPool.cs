using UniRx.Toolkit;
using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : ObjectPool<EnemyController>
{
    private readonly EnemyController _enemyPrefab = null;
    private readonly Transform _parent = null;
    List<EnemyController> _enemyList = new List<EnemyController>();
    public List<EnemyController> EnemyList => _enemyList;

    /// <summary>
    /// ��������I�u�W�F�N�g�Ɛ������̐e������������R���X�g���N�^
    /// </summary>
    /// <param name="prefab">��������I�u�W�F�N�g</param>
    /// <param name="objectParent">�������̐e</param>
    public EnemyPool(EnemyController prefab,Transform objectParent)
    {
        _enemyPrefab = prefab;
        _parent = objectParent;

    }
    protected override EnemyController CreateInstance()
    {
        var enemy = GameObject.Instantiate(_enemyPrefab);
        _enemyList.Add(enemy);
        enemy.transform.SetParent(_parent);

        return enemy;
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        _enemyList.Remove(enemy);
    }

}
