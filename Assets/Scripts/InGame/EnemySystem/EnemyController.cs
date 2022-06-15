using System;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDestroy
{
    Rigidbody _rigidbody;

    [SerializeField, Tooltip("ドロップする経験値")]
    GameObject _dropItem = null;

    [SerializeField]
    int _initHP = 3;

    int _hp = 0;

    [SerializeField, Tooltip("プレイヤーに与えるダメージ")]
    int _damage = 1;
    //bool _isMove = true;

    private void Start()
    {
        if (!_dropItem)
        {
            _dropItem = Resources.Load<GameObject>("dango");
        }
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {

        Move();

    }

    private void Move()
    {
        if (!GameManager.Instance.IsPauseFlag)
        {
            Vector3 dir = GameManager.Instance.Player.transform.position - transform.position;

            this.transform.forward = dir;
            _rigidbody.velocity = dir.normalized * 3f;
        }
    }

    public void SetPosition(Vector3 position)
    {
        this.transform.position = position;
        _hp = _initHP;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ChangeHP(-(_damage));         
        }
    }

    public void Damage(int dmg)
    {
        _hp -= dmg;
        if (_hp<=0)
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        Instantiate(_dropItem, this.transform.position, Quaternion.identity);
        GameManager.Instance.EnemyManager.EnemyPool.Return(this);
    }
}
