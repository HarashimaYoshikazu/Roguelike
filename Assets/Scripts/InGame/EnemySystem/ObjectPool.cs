using UniRx.Toolkit;
using UnityEngine;

public class ObjectPool : ObjectPool<EnemyController>
{
    private readonly EnemyController _enemyPrefab = null;
    private readonly Transform _parent = null;

    /// <summary>
    /// ��������I�u�W�F�N�g�Ɛ������̐e������������R���X�g���N�^
    /// </summary>
    /// <param name="prefab">��������I�u�W�F�N�g</param>
    /// <param name="objectParent">�������̐e</param>
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
