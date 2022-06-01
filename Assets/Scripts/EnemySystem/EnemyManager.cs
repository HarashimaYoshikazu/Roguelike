using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private EnemyController _enemyPrefab;

    [SerializeField]
    private Transform _hierarchyTransform;

    private ObjectPool _effectPool; 

    void Start()
    {
        //オブジェクトプールを生成
        _effectPool = new ObjectPool(_enemyPrefab,_hierarchyTransform);

        //破棄されたときにPoolを解放する
        this.OnDestroyAsObservable().Subscribe(_ => _effectPool.Dispose());

        //ボタンが押されたらエフェクト生成
        _button.OnClickAsObservable()
            .Subscribe(_ =>
            {
                    //ランダムな場所
                    var position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

                    //poolから1つ取得
                    var effect = _effectPool.Rent();

                    //エフェクトを再生し、再生終了したらpoolに返却する
                    effect.SetPosition(position)
                    .Subscribe(__ =>
                    {
                        _effectPool.Return(effect);
                    });
            });
    }
}
