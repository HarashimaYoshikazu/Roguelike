using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    static EnemyManager _instans = null;
    public static EnemyManager Instans => _instans;

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
        GameManager.Instance.PauseAction += OnPause;
        GameManager.Instance.ResumeAction += OnResume;
    }
    private void OnDestroy()
    {
        GameManager.Instance.PauseAction -= OnPause;
        GameManager.Instance.ResumeAction -= OnResume;
    }

    [SerializeField]
    private Button _button;

    [SerializeField]
    private EnemyController _enemyPrefab;

    [SerializeField]
    private Transform _hierarchyTransform;

    private EnemyPool _enemyPool;
    public EnemyPool Pool => _enemyPool;


    void Start()
    {
        //オブジェクトプールを生成
        _enemyPool = new EnemyPool(_enemyPrefab, _hierarchyTransform);

        //破棄されたときにPoolを解放する
        this.OnDestroyAsObservable().Subscribe(_ => _enemyPool.Dispose()).AddTo(this);

        //ボタンが押されたら生成
        _button.OnClickAsObservable()
            .Subscribe(_ =>
            {
                EnemyRent();

            }).AddTo(this);
    }
    void EnemyRent()
    {
        Debug.Log(GameManager.Instance.Player.transform.position);
        float rand = Random.Range(0f, 360f);
        Vector3 vec = CircleUtil.GetCirclePosition(rand, 10f, GameManager.Instance.Player.transform.position);
        vec.y = 1f;
        //poolから1つ取得
        var enemy = _enemyPool.Rent();

        //敵のポジションを設定
        enemy.SetPosition(vec);

    }

    void OnPause()
    {
        foreach(var i in _enemyPool.EnemyList)
        {
            i.Pause();
        }
    }

    void OnResume()
    {
        foreach (var i in _enemyPool.EnemyList)
        {
            i.Resume();
        }
    }
}
