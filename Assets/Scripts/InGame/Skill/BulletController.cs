using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletController : MonoBehaviour, IDestroy
{
    [SerializeField]
    float _initSpeed = 5f;
    float _speed = 0f;
    [SerializeField, Tooltip("ìGÇ…ó^Ç¶ÇÈÉ_ÉÅÅ[ÉW")]
    int _damage = 1;

    Vector3 _targetVec;
    [SerializeField]
    float _interval = 4f;
    float _timer = 0f;
    Rigidbody _rb;

    EnemyController _target;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _speed = _initSpeed;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsPauseFlag)
        {
            return;
        }
        //_speed = Random.Range(_initSpeed - 4f, _initSpeed + 10f);
        transform.position += _targetVec * _speed * Time.deltaTime;

        _timer += Time.deltaTime;
        if (_timer > _interval)
        {
            _timer = 0f;
            DestroyObject();
        }
    }

    public void Shoot(GameObject obj)
    {
        _target = obj.GetComponent<EnemyController>();
        if (!_target)
        {
            return;
        }
        Vector3 vec = GameManager.Instance.Player.transform.position;
        vec.y = 0.5f;
        this.transform.position = vec;

        //TODO objÇ‹Ç≈ìÆÇ≠
        Vector3 dis = obj.transform.position - this.transform.position  ;
        _targetVec = dis;
        _targetVec.y = 0f;
        _targetVec.Normalize();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyController enemy) &&!GameManager.Instance.IsPauseFlag)
        {          
            enemy.Damage(_damage);
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        GameManager.Instance.Player.BulletPool.Return(this);
    }
}
