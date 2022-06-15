using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Net : MonoBehaviour
{
    [SerializeField]
    int _initDamage = 1;
    int _damage = 0;

    [SerializeField]
    float _initEffectTime = 1f;
    float _effectTime = 0f;
    public float EffectTime => _effectTime; 

    private void Awake()
    {
        ChangeDamage(_initDamage);
        ChangeEffectTime(_initEffectTime);
        this.gameObject.SetActive(false);
    }
    private async void OnEnable()
    {
        bool active = await ActiveFalseTask(_effectTime);
        this.gameObject.SetActive(active);
    }

    private async UniTask<bool> ActiveFalseTask(float waitTime)
    {      
        await UniTask.Delay((int)(waitTime * 1000));        
        return false;
    }

    public void ChangeDamage(int value)
    {
        _damage += value;
    }

    public void ChangeEffectTime(float value)
    {
        _effectTime += value;
    }

    float _timer = 0f;
    [SerializeField]
    float _interval = 1f;
    private void OnTriggerStay(Collider other)
    {
        _timer+= Time.deltaTime;
        if (other.TryGetComponent(out EnemyController enemy) && _timer>_interval)
        {
            enemy.Damage(_damage);
            _timer = 0f;
        }
    }
}
