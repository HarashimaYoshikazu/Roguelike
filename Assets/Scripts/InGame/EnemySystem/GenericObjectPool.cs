using UniRx.Toolkit;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// �I�u�W�F�N�g�v�[���N���X
/// </summary>
/// <typeparam name="T">Destroy�C���^�[�t�F�C�X�A�R���|�[�l���g�N���X���p������K�v������</typeparam>
public class GenericObjectPool<T> : ObjectPool<T> where T : UnityEngine.Component, IDestroy
{
    private readonly List<T> _objectPrefabList = new List<T>();
    private readonly Transform _parent = null;
    List<T> _objectList = new List<T>();
    public List<T> ObjectList => _objectList;

    List<int> _probList = new List<int>();

    /// <summary>
    /// ��������I�u�W�F�N�g�Ɛ������̐e������������R���X�g���N�^
    /// </summary>
    /// <param name="prefab">��������I�u�W�F�N�g</param>
    /// <param name="objectParent">�������̐e</param>
    public GenericObjectPool(T prefab, Transform objectParent)
    {
        _objectPrefabList.Add(prefab);
        _parent = objectParent;

    }

    /// <summary>
    /// ��������I�u�W�F�N�g�Ɛ������̐e������������R���X�g���N�^
    /// </summary>
    /// <param name="prefab">��������I�u�W�F�N�g</param>
    /// <param name="objectParent">�������̐e</param>
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
            if (rand < _probList[i]) //���I�ɓ���������
            {
                obj = GameObject.Instantiate(_objectPrefabList[i]);
                _objectList.Add(obj);
                obj.transform.SetParent(_parent);
                //��������obj�𐶐�
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
