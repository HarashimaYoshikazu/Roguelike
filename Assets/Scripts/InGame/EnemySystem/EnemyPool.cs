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
    /// 生成するオブジェクトと生成元の親を初期化するコンストラクタ
    /// </summary>
    /// <param name="prefab">生成するオブジェクト</param>
    /// <param name="objectParent">生成元の親</param>
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
