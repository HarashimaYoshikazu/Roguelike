using UniRx.Toolkit;
using UnityEngine;

public class ObjectPool : ObjectPool<EnemyController>
{
    private readonly EnemyController _enemyPrefab = null;
    private readonly Transform _parent = null;

    /// <summary>
    /// 生成するオブジェクトと生成元の親を初期化するコンストラクタ
    /// </summary>
    /// <param name="prefab">生成するオブジェクト</param>
    /// <param name="objectParent">生成元の親</param>
    public ObjectPool(EnemyController prefab,Transform objectParent)
    {
        _enemyPrefab = prefab;
        _parent = objectParent;

    }
    protected override EnemyController CreateInstance()
    {
        var enemy = GameObject.Instantiate(_enemyPrefab);
        enemy.transform.SetParent(_parent);

        return enemy;
    }
}
