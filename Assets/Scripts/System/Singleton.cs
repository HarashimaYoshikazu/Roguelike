using System;
using UnityEngine;

/// <summary>
/// MonoBehavior���p�����Ȃ�Singleton�N���X
/// </summary>
/// <typeparam name="T">instance���쐬����h���N���X</typeparam>
public class Singleton<T>
                    : IDisposable
                    where T : Singleton<T>, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            return GetOrCreateInstance<T>();
        }
    }

    protected static InheritSingletonType GetOrCreateInstance<InheritSingletonType>()
        where InheritSingletonType : class, T, new()
    {
        if (IsCreated)
        {
            // ���N���X����Ă΂ꂽ��Ɍp���悩��Ă΂��ƃG���[�ɂȂ�B��Ɍp���悩��Ă�
            if (!typeof(InheritSingletonType).IsAssignableFrom(_instance.GetType()))
            {
                    Debug.LogErrorFormat(
                    "{1}��{0}���p�����Ă��܂���",
                    typeof(InheritSingletonType),
                    _instance.GetType()
                );
            }
        }
        else
        {
            _instance = new InheritSingletonType();
        }
        return _instance as InheritSingletonType;
    }

    public static bool IsCreated
    {
        get { return _instance != null; }
    }

    public virtual void Dispose()
    {
        _instance = default;
    }

    /// <summary>
    /// �R���X�g���N�^�i�O������̌Ăяo���֎~�j
    /// </summary>
    protected Singleton() { }
}