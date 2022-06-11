using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField, Tooltip("")]
    GameObject _pauseTextPanel = null;

    private void Awake()
    {
        GameManager.Instance.SetUIManager(this);
    }

    public void SetActiveText(bool isActive)
    {
        _pauseTextPanel.SetActive(isActive);
    }
}
