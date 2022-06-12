using UniRx.Toolkit;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// オブジェクトプールクラス
/// </summary>
/// <typeparam name="T">Destroyインターフェイス、コンポーネントクラスを継承する必要がある</typeparam>
public class GenericObjectPool<T> : ObjectPool<T> where T : UnityEngine.Component,IDestroy
{
    private readonly T _objectPrefab = null;
    private readonly Transform _parent = null;
    List<T> _objectList = new List<T>();
    public List<T> ObjectList => _objectList;

    /// <summary>
    /// 生成するオブジェクトと生成元の親を初期化するコンストラクタ
    /// </summary>
    /// <param name="prefab">生成するオブジェクト</param>
    /// <param name="objectParent">生成元の親</param>
    public GenericObjectPool(T prefab,Transform objectParent)
    {
        _objectPrefab = prefab;
        _parent = objectParent;

    }
    protected override T CreateInstance()
    {
        var obj = GameObject.Instantiate(_objectPrefab);
        _objectList.Add(obj);
        obj.transform.SetParent(_parent);

        return obj;
    }

    public void ReturnAllObject()
    {
        foreach(var i in _objectList)
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
