using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SkillButton : MonoBehaviour
{
    [SerializeField, Tooltip("スキルを選択するためのシーン上のボタンオブジェクトのリスト")]
    List<GameObject> _selectButtonList = new List<GameObject>();

    /// <summary>スキル選択ボタンに付いているテキストコンポーネントのリスト</summary>
    List<Text> _selectTextList = new List<Text>();

    /// <summary>ランダムに選ばれるスキルクラスを格納するリスト</summary>
    List<SkillInfo> _selectedSkill = new List<SkillInfo>();

    [SerializeField, Tooltip("スキルボタンの親オブジェクトパネル")]
    GameObject _selectPanel = null;

    private void Awake()
    {

    }

    private void Start()
    {
        for (int i = 0; i < _selectButtonList.Count; i++)
        {
            _selectedSkill.Add(null);
            _selectTextList.Add(_selectButtonList[i].GetComponentInChildren<Text>());

            Button button = _selectButtonList[i].GetComponent<Button>();
            button.onClick.AddListener(() =>
            { OnClick(i); });
            
            
        }
    }

    public void SelectStart()
    {
        //_canvas.alpha = 1;
        _selectPanel.SetActive(true);

        List<SkillInfo> table = new List<SkillInfo>();
        var list = GameData.SkillSelectTable
            .Where(s => GameManager.Instance.Level >= s.Level); //スキルの種類を格納

        int totalProb = list.Sum(s => s.Probability);
        int rand = Random.Range(0, totalProb);

        //初期化
        for (int i = 0; i < _selectButtonList.Count; ++i)
        {
            _selectedSkill[i] = null;
            _selectTextList[i].text = "";
        }

        for (int i = 0; i < _selectButtonList.Count; ++i)
        {
            foreach (var s in list)
            {
                if (rand < s.Probability) //抽選に当たったら
                {
                    _selectedSkill[i] = s;
                    _selectTextList[i].text = s.Name;
                    list = list.Where(ls => !(ls.Type == s.Type && ls.TypeID == s.TypeID)); //当たったスキルを省く
                    break;
                }
                rand -= s.Probability;
            }
        }
    }

    void OnClick(int selectedIndex)
    {
        Debug.Log($"カウント{_selectedSkill.Count}");
        Debug.Log($"引数{selectedIndex}");
        GameManager.Instance.LevelUpSelect(_selectedSkill[selectedIndex-1]);
        _selectPanel.SetActive(false);
        GameManager.Instance.GameCycle.StateMachine.Dispatch((int)StateEvent.GameStart);
    }

    /// <summary>
    /// テスト用の経験値獲得関数
    /// </summary>
    public void GetEXPDebug(int value)
    {
        GameManager.Instance.GetExp(value);
    }
}
