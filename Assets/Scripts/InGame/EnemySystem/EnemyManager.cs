using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{

    [SerializeField,Tooltip("デバッグ用")]
    Button _button;

    [Header("敵関係")]
    [SerializeField,Tooltip("敵のプレハブ")]
    EnemyController[] _enemyPrefabs;

    [SerializeField,Tooltip("敵の生成先の親オブジェクト")]
    Transform _enemyParent;

    float _timer = 0f;
    [SerializeField, Tooltip("タイマーのインターバル")]
    float _interval = 2f;

    [Header("経験値関係")]
    [SerializeField, Tooltip("敵が落とす経験値のプレハブ")]
    Exp _expPrefab;

    [SerializeField, Tooltip("敵が落とす経験値の生成先の親オブジェクト")]
    Transform _expParent;

    /// <summary>敵のプール</summary>
    GenericObjectPool<EnemyController> _enemyPool;
    public GenericObjectPool<EnemyController> EnemyPool => _enemyPool;

    /// <summary>敵が落とす経験値のプール</summary>
    GenericObjectPool<Exp> _expObjectPool;
    public GenericObjectPool<Exp> ExpPool => _expObjectPool;

    private void Awake()
    {
        GameManager.Instance.SetEnemyManager(this);

        //オブジェクトプールを生成
        _enemyPool = new GenericObjectPool<EnemyController>(_enemyPrefabs[0], _enemyParent);

        _expObjectPool = new GenericObjectPool<Exp>(_expPrefab,_expParent);

        //破棄されたときにPoolを解放する
        this.OnDestroyAsObservable().Subscribe(_ => _enemyPool.Dispose()).AddTo(this);
        this.OnDestroyAsObservable().Subscribe(_ => _expObjectPool.Dispose()).AddTo(this);

        //ボタンが押されたら生成
        _button.OnClickAsObservable()
            .Subscribe(_ =>
            {
                EnemyRent();

            }).AddTo(this);

    }

    private void FixedUpdate()
    {
        //Debug.Log("数"+_enemyPool.ObjectList.Count);
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
        //poolから1つ取得
        var enemy = _enemyPool.Rent();

        //敵のポジションを設定
        enemy.SetPosition(vec);

    }

    public void ResetAllEnemy()
    {
       _enemyPool.ReturnAllObject();
        _expObjectPool.ReturnAllObject();
    }
}
