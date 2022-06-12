using UniRx.Toolkit;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// �I�u�W�F�N�g�v�[���N���X
/// </summary>
/// <typeparam name="T">Destroy�C���^�[�t�F�C�X�A�R���|�[�l���g�N���X���p������K�v������</typeparam>
public class GenericObjectPool<T> : ObjectPool<T> where T : UnityEngine.Component,IDestroy
{
    private readonly T _objectPrefab = null;
    private readonly Transform _parent = null;
    List<T> _objectList = new List<T>();
    public List<T> ObjectList => _objectList;

    /// <summary>
    /// ��������I�u�W�F�N�g�Ɛ������̐e������������R���X�g���N�^
    /// </summary>
    /// <param name="prefab">��������I�u�W�F�N�g</param>
    /// <param name="objectParent">�������̐e</param>
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
