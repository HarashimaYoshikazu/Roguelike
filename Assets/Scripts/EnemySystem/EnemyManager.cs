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
        //�I�u�W�F�N�g�v�[���𐶐�
        _effectPool = new ObjectPool(_enemyPrefab,_hierarchyTransform);

        //�j�����ꂽ�Ƃ���Pool���������
        this.OnDestroyAsObservable().Subscribe(_ => _effectPool.Dispose());

        //�{�^���������ꂽ��G�t�F�N�g����
        _button.OnClickAsObservable()
            .Subscribe(_ =>
            {
                    //�����_���ȏꏊ
                    var position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

                    //pool����1�擾
                    var effect = _effectPool.Rent();

                    //�G�t�F�N�g���Đ����A�Đ��I��������pool�ɕԋp����
                    effect.SetPosition(position)
                    .Subscribe(__ =>
                    {
                        _effectPool.Return(effect);
                    });
            });
    }
}
