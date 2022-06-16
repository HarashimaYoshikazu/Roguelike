using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    bool _canClick = false;
    [SerializeField]
    string _sceneName = "GameScene";
    void Update()
    {
        if (_canClick && Input.anyKeyDown)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }

    public void CanClick()
    {
        _canClick = true;
    }
}
