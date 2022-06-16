using UniRx.Toolkit;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// オブジェクトプールクラス
/// </summary>
/// <typeparam name="T">Destroyインターフェイス、コンポーネントクラスを継承する必要がある</typeparam>
public class GenericObjectPool<T> : ObjectPool<T> where T : UnityEngine.Component, IDestroy
{
    private readonly List<T> _objectPrefabList = new List<T>();
    private readonly Transform _parent = null;
    List<T> _objectList = new List<T>();
    public List<T> ObjectList => _objectList;

    List<int> _probList = new List<int>();

    /// <summary>
    /// 生成するオブジェクトと生成元の親を初期化するコンストラクタ
    /// </summary>
    /// <param name="prefab">生成するオブジェクト</param>
    /// <param name="objectParent">生成元の親</param>
    public GenericObjectPool(T prefab, Transform objectParent)
    {
        _objectPrefabList.Add(prefab);
        _parent = objectParent;

    }

    /// <summary>
    /// 生成するオブジェクトと生成元の親を初期化するコンストラクタ
    /// </summary>
    /// <param name="prefab">生成するオブジェクト</param>
    /// <param name="objectParent">生成元の親</param>
    public GenericObjectPool(T prefab, int prob, Transform objectParent)
    {
        _objectPrefabList.Add(prefab);
        _probList.Add(prob);
        _parent = objectParent;

    }


    public void AddPrefab(T prefab, int prob)
    {
        _objectPrefabList.Add(prefab);
        _probList.Add(prob);
    }

    protected override T CreateInstance()
    {
        int totalProb = _probList.Sum();
        int rand = Random.Range(0, totalProb);

        T obj = _objectPrefabList[0];

        for (int i = 0; i < _objectList.Count; i++)
        {
            if (rand < _probList[i]) //抽選に当たったら
            {
                obj = GameObject.Instantiate(_objectPrefabList[i]);
                _objectList.Add(obj);
                obj.transform.SetParent(_parent);
                //当たったobjを生成
                break;
            }
            rand -= _probList[i];
        }


        return obj;
    }

    public void ReturnAllObject()
    {
        foreach (var i in _objectList)
        {
            i.DestroyObject();
        }
    }

    public void RemoveEnemy(T obj)
    {
        _objectList.Remove(obj);
    }

}

public interface IDestroy
{
    void DestroyObject();
}
