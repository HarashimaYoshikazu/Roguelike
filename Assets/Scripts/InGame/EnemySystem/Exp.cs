using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour,IDestroy
{
    [SerializeField,Tooltip("Šl“¾‚·‚éŒoŒ±’l")]
    int _addExp = 1;

    public void DestroyObject()
    {
        GameManager.Instance.EnemyManager.ExpPool.Return(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GetExp(_addExp);
            DestroyObject();
        }
    }
}
