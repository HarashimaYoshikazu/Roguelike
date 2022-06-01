using System;
using UnityEngine;
using UniRx;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float _timer = 1.0f;
    public IObservable<Unit> SetPosition(Vector3 position)
    {
        Vector3 lastPos = this.transform.position;
        this.transform.position = position;

        return Observable.Timer(TimeSpan.FromSeconds(_timer))
                .ForEachAsync(_ => this.transform.position = lastPos);
    }
}
