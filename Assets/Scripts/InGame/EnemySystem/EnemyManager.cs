using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    static EnemyManager _instans = null;
    public static EnemyManager Instans => _instans;

    float _timer = 0f;
    [SerializeField, Tooltip("�^�C�}�[�̃C���^�[�o��")]
    float _interval = 2f;

    private void Awake()
    {
        if (!_instans)
        {
            _instans = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //�I�u�W�F�N�g�v�[���𐶐�
        _enemyPool = new EnemyPool(_enemyPrefab, _hierarchyTransform);

        //�j�����ꂽ�Ƃ���Pool���������
        this.OnDestroyAsObservable().Subscribe(_ => _enemyPool.Dispose()).AddTo(this);

        //�{�^���������ꂽ�琶��
        _button.OnClickAsObservable()
            .Subscribe(_ =>
            {
                EnemyRent();

            }).AddTo(this);
        
    }

    [SerializeField]
    private Button _button;

    [SerializeField]
    private EnemyController _enemyPrefab;

    [SerializeField]
    private Transform _hierarchyTransform;

    private EnemyPool _enemyPool;
    public EnemyPool Pool => _enemyPool;


    private void FixedUpdate()
    {
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
        Debug.Log(GameManager.Instance.Player.transform.position);
        float rand = Random.Range(0f, 360f);
        Vector3 vec = CircleUtil.GetCirclePosition(rand, 10f, GameManager.Instance.Player.transform.position);
        vec.y = 1f;
        //pool����1�擾
        var enemy = _enemyPool.Rent();

        //�G�̃|�W�V������ݒ�
        enemy.SetPosition(vec);

    }

    public void ResetAllEnemy()
    {
       _enemyPool.ReturnAllEnemy();
    }
}
