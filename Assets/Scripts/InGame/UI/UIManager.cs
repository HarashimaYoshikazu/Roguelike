using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField, Tooltip("パネル")]
    Text _pauseTextPanel = null;

    [SerializeField, Tooltip("HPのスライダー")]
    Slider _hpSlider = null;

    [SerializeField, Tooltip("経験値スライダー")]
    Slider _expSlider = null;

    private void Awake()
    {
        GameManager.Instance.SetUIManager(this);   
    }

    private void Start()
    {
        _hpSlider.maxValue = GameManager.Instance.HP;
    }

    public void SetActiveText(bool isActive,string text)
    {
        _pauseTextPanel.text = text;
        _pauseTextPanel.gameObject.SetActive(isActive);
    }

    public void SetExpMaxValue(int value)
    {
        _expSlider.maxValue = (float)value;
    }

    public void  UpdateHPSlider(int value)
    {
        _hpSlider.value = value;
    }

    public void UpdateExpSlider(int value)
    {
        _expSlider.value = value;
    }
}
