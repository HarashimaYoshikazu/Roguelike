using System;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDestroy
{

    [SerializeField, Tooltip("ドロップする経験値")]
    GameObject _dropItem = null;

    [SerializeField]
    int _initHP = 3;

    int _hp = 0;

    [SerializeField, Tooltip("プレイヤーに与えるダメージ")]
    int _damage = 1;


    private void Start()
    {
        if (!_dropItem)
        {
            _dropItem = Resources.Load<GameObject>("dango");
        }
    }
    private void FixedUpdate()
    {

        Move();

    }

    Vector3 _velocity;
    private void Move()
    {
        if (!GameManager.Instance.IsPauseFlag)
        {
            _velocity += (GameManager.Instance.Player.transform.position - transform.position) * 3;
            _velocity *= 0.5f;
            _velocity.Normalize();
            transform.position += _velocity * Time.deltaTime;
            this.transform.forward = _velocity;
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
        Vector3 vec = this.transform.position;
        vec.y = 0.5f;
        Instantiate(_dropItem, vec, Quaternion.identity);
        GameManager.Instance.EnemyManager.EnemyPool.Return(this);
    }
}
