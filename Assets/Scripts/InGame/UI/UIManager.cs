using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    bool isPause = false;
    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape) &&!isPause)
        {
            GameManager.Instance.Pause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            GameManager.Instance.Resume();
        }
        
    }
}
