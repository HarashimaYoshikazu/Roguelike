using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("パラメータ")]
    [SerializeField, Tooltip("動く速さ")]
    float _initSpeed = 5f;

    [Header("コンポーネント")]
    [SerializeField,Tooltip("PlayerのRigidBodyコンポーネント")]
    Rigidbody _rb = null;
    [SerializeField,Tooltip("PlayerのAnimatorコンポーネント")]
    Animator _anim = null;

    float _speed = 0f;
    float v;
    float h;

    private void Awake()
    {
        _speed = _initSpeed;
        if (!_rb)
        {
            _rb = GetComponent<Rigidbody>();
        }
        if (!_anim)
        {
            _anim = GetComponent<Animator>();
        }     
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsPauseFlag)
        {
            Move();
        } 
    }

    private void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        if (dir != Vector3.zero)
        {
            this.transform.forward = dir;
        }

        _rb.velocity = dir.normalized * _speed + _rb.velocity.y * Vector3.up;
    }
}
