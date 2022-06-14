using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletController : MonoBehaviour, IDestroy
{
    [SerializeField]
    float _initSpeed = 5f;
    float _speed = 0f;
    [SerializeField, Tooltip("“G‚É—^‚¦‚éƒ_ƒ[ƒW")]
    int _damage = 1;

    Vector3 _targetVec;
    [SerializeField]
    float _interval = 4f;
    float _timer = 0f;

    EnemyController _target;
    private void Start()
    {
        _speed = _initSpeed;
    }

    void Update()
    {
        Debug.Log(_speed);
        _speed = Random.Range(_initSpeed-4f, _initSpeed+10f);
        transform.position += _targetVec * _speed * Time.deltaTime;

        _target = null;


        _timer += Time.deltaTime;
        if (_timer>_interval)
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

        //TODO obj‚Ü‚Å“®‚­
        Vector3 dis = obj.transform.position - this.transform.position  ;
        _targetVec = dis;
        _targetVec.Normalize();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyController enemy))
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
