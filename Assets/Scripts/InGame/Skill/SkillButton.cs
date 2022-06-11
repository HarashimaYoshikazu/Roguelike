using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SkillButton : MonoBehaviour
{
    [SerializeField, Tooltip("�X�L����I�����邽�߂̃V�[����̃{�^���I�u�W�F�N�g�̃��X�g")]
    List<GameObject> _selectButtonList = new List<GameObject>();

    /// <summary>�X�L���I���{�^���ɕt���Ă���e�L�X�g�R���|�[�l���g�̃��X�g</summary>
    List<Text> _selectTextList = new List<Text>();

    /// <summary>�����_���ɑI�΂��X�L���N���X���i�[���郊�X�g</summary>
    List<SkillInfo> _selectedSkill = new List<SkillInfo>();

    [SerializeField, Tooltip("�X�L���{�^���̐e�I�u�W�F�N�g�p�l��")]
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
            .Where(s => GameManager.Instance.Level >= s.Level); //�X�L���̎�ނ��i�[

        int totalProb = list.Sum(s => s.Probability);
        int rand = Random.Range(0, totalProb);

        //������
        for (int i = 0; i < _selectButtonList.Count; ++i)
        {
            _selectedSkill[i] = null;
            _selectTextList[i].text = "";
        }

        for (int i = 0; i < _selectButtonList.Count; ++i)
        {
            foreach (var s in list)
            {
                if (rand < s.Probability) //���I�ɓ���������
                {
                    _selectedSkill[i] = s;
                    _selectTextList[i].text = s.Name;
                    list = list.Where(ls => !(ls.Type == s.Type && ls.TypeID == s.TypeID)); //���������X�L�����Ȃ�
                    break;
                }
                rand -= s.Probability;
            }
        }
    }

    void OnClick(int selectedIndex)
    {
        Debug.Log($"�J�E���g{_selectedSkill.Count}");
        Debug.Log($"����{selectedIndex}");
        GameManager.Instance.LevelUpSelect(_selectedSkill[selectedIndex-1]);
        _selectPanel.SetActive(false);
        GameManager.Instance.GameCycle.StateMachine.Dispatch((int)StateEvent.GameStart);
    }

    /// <summary>
    /// �e�X�g�p�̌o���l�l���֐�
    /// </summary>
    public void GetEXPDebug(int value)
    {
        GameManager.Instance.GetExp(value);
    }
}
