using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    float _distance = 0f;
    Player _player;
    [SerializeField]
    float _setFalseDistance = 10f;

    [SerializeField]
    GameObject _object;

    void Start()
    {
        _player = GameManager.Instance.Player;
    }
    
    private void FixedUpdate()
    {
        _distance = Vector3.Distance(this.transform.position,_player.transform.position);
        if (_distance<_setFalseDistance)
        {
            if (_object)
            {
                _object.SetActive(true);
            }
        }
        else
        {
            if (_object)
            {
                _object.SetActive(false);
            }
        }
    }
}
