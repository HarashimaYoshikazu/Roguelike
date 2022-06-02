using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class EnemyManager : MonoBehaviour
{
    static EnemyManager _instans = null;
    public static EnemyManager Instans => _instans;
    private void Awake()
    {
        if(!_instans)
        {
            _instans = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private Button _button;

    [SerializeField]
    private EnemyController _enemyPrefab;

    [SerializeField]
    private Transform _hierarchyTransform;

    private ObjectPool _enemyPool;
    public ObjectPool Pool => _enemyPool;

    void Start()
    {
        //オブジェクトプールを生成
        _enemyPool = new ObjectPool(_enemyPrefab,_hierarchyTransform);
    
        //破棄されたときにPoolを解放する
        this.OnDestroyAsObservable().Subscribe(_ => _enemyPool.Dispose());

        //ボタンが押されたらエフェクト生成
        _button.OnClickAsObservable()
            .Subscribe(_ =>
            {
                    //ランダムな場所
                    var position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

                    //poolから1つ取得
                    var effect = _enemyPool.Rent();

                //エフェクトを再生し、再生終了したらpoolに返却する
                effect.SetPosition(position);
            });
    }
}
