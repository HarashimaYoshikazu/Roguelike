using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody _rigidbody;
    Transform _player = null;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 dir = _player.position - transform.position ;

        _rigidbody.velocity = dir.normalized * 3f;
    }
    public void SetPosition(Vector3 position)
    {
        this.transform.position = position;
    }

    public void Death()
    {
        EnemyManager.Instans.Pool.Return(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Death();
        }
    }
}
