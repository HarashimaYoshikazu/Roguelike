using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField, Tooltip("ƒpƒlƒ‹")]
    UnityEngine.UI.Text _pauseTextPanel = null;

    private void Awake()
    {
        GameManager.Instance.SetUIManager(this);
    }

    public void SetActiveText(bool isActive,string text)
    {
        _pauseTextPanel.text = text;
        _pauseTextPanel.gameObject.SetActive(isActive);
    }
}
