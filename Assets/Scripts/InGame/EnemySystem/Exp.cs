using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    [SerializeField,Tooltip("�l������o���l")]
    int _addExp = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GetExp(_addExp);
            Destroy(this.gameObject);
        }
    }
}
