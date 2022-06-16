using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{

    [SerializeField,Tooltip("�f�o�b�O�p")]
    Button _button;

    [Header("�G�֌W")]
    [SerializeField,Tooltip("�G�̃v���n�u")]
    EnemyController[] _enemyPrefabs;

    [SerializeField,Tooltip("�G�̐�����̐e�I�u�W�F�N�g")]
    Transform _enemyParent;

    float _timer = 0f;
    [SerializeField, Tooltip("�^�C�}�[�̃C���^�[�o��")]
    float _interval = 2f;

    [Header("�o���l�֌W")]
    [SerializeField, Tooltip("�G�����Ƃ��o���l�̃v���n�u")]
    Exp _expPrefab;

    [SerializeField, Tooltip("�G�����Ƃ��o���l�̐�����̐e�I�u�W�F�N�g")]
    Transform _expParent;

    /// <summary>�G�̃v�[��</summary>
    GenericObjectPool<EnemyController> _enemyPool;
    public GenericObjectPool<EnemyController> EnemyPool => _enemyPool;

    /// <summary>�G�����Ƃ��o���l�̃v�[��</summary>
    GenericObjectPool<Exp> _expObjectPool;
    public GenericObjectPool<Exp> ExpPool => _expObjectPool;

    private void Awake()
    {
        GameManager.Instance.SetEnemyManager(this);

        //�I�u�W�F�N�g�v�[���𐶐�
        _enemyPool = new GenericObjectPool<EnemyController>(_enemyPrefabs[0], _enemyParent);

        _expObjectPool = new GenericObjectPool<Exp>(_expPrefab,_expParent);

        //�j�����ꂽ�Ƃ���Pool���������
        this.OnDestroyAsObservable().Subscribe(_ => _enemyPool.Dispose()).AddTo(this);
        this.OnDestroyAsObservable().Subscribe(_ => _expObjectPool.Dispose()).AddTo(this);

        //�{�^���������ꂽ�琶��
        _button.OnClickAsObservable()
            .Subscribe(_ =>
            {
                EnemyRent();

            }).AddTo(this);

    }

    private void FixedUpdate()
    {
        //Debug.Log("��"+_enemyPool.ObjectList.Count);
        if (!GameManager.Instance.IsPauseFlag)
        {
            _timer += Time.deltaTime;
            if (_timer > _interval)
            {
                _timer = 0f;
                EnemyRent();
            }
        }

    }
    void EnemyRent()
    {
        //Debug.Log(GameManager.Instance.Player.transform.position);
        float rand = Random.Range(0f, 360f);
        Vector3 vec = CircleUtil.GetCirclePosition(rand, 10f, GameManager.Instance.Player.transform.position);
        vec.y = 0f;
        //pool����1�擾
        var enemy = _enemyPool.Rent();

        //�G�̃|�W�V������ݒ�
        enemy.SetPosition(vec);

    }

    public void ResetAllEnemy()
    {
       _enemyPool.ReturnAllObject();
        _expObjectPool.ReturnAllObject();
    }
}
