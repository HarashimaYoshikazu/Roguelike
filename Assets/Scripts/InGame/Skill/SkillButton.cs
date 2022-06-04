using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _selectButtonList = new List<GameObject>();
    [SerializeField]
    List<SkillInfo> _selectSkill = new List<SkillInfo>();

    private void Start()
    {
        for(int i = 0;i<_selectButtonList.Count;i++)
        {
            Button button = _selectButtonList[i].GetComponent<Button>();
            Text text = _selectButtonList[i].GetComponent<Text>();
            button.onClick.AddListener(() =>
            { OnClick(); });

        }
    }

    void OnClick()
    {
        //GameManager.Instance.LevelUpSelect();
    }
}
