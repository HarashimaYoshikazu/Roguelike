using UniRx;
using UnityEngine;
using System;
using State = StateMachine<GameCycle>.State;

public class GameCycle : MonoBehaviour
{
    StateMachine<GameCycle> _stateMachine = null;
    public StateMachine<GameCycle> StateMachine => _stateMachine;

    [SerializeField]
    GameObject[] _buttons;

    private void Awake()
    {
        GameManager.Instance.SetGameCycle(this);
    }

    private void Start()
    {
        //GameManager.Instance.SetUP();
        //�������R���X�g���N�^�͂��̃N���X��ۑ�
        _stateMachine = new StateMachine<GameCycle>(this);

        //�J�ڂ�o�^
        _stateMachine.AddTransition<StartState, InGameState>((int)StateEvent.GameStart);
        _stateMachine.AddTransition<InGameState, ResultState>((int)StateEvent.GameOver);
        _stateMachine.AddTransition<InGameState, LevelUpState>((int)StateEvent.LevelUp);
        _stateMachine.AddTransition<LevelUpState, InGameState>((int)StateEvent.GameStart);
        _stateMachine.AddTransition<InGameState, PauseState>((int)StateEvent.Pause);
        _stateMachine.AddTransition<PauseState, InGameState>((int)StateEvent.Resume);
        _stateMachine.AddTransition<ResultState, StartState>((int)StateEvent.ReStart);

        //�ŏ���State��ݒ�AEnte�֐����Ăяo��
        _stateMachine.StartSetUp<StartState>();
    }

    public void GoIngame()
    {
        _stateMachine.Dispatch((int)StateEvent.GameStart);
        _buttons[0].SetActive(false);
    }
    public void GoResult()
    {
        _stateMachine.Dispatch((int)StateEvent.GameOver);
        _buttons[1].SetActive(true);
    }

    public void GoStart()
    {
        _stateMachine.Dispatch((int)StateEvent.ReStart);
        _buttons[1].SetActive(false);
        _buttons[0].SetActive(true);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    class StartState : State
    {
        protected override void OnEnter(State prevState)
        {
            //Debug.Log("�͂��܂�Enter");            
            GameManager.Instance.IsPause(true,"�{�^���ŊJ�n");
            
        }

        protected override void OnUpdate()
        {
            //Debug.Log("�͂��܂�Update");
        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("�͂��܂�Exit");
            GameManager.Instance.IsPause(false,"");
        }
    }

    class PauseState : State
    {
        protected override void OnEnter(State prevState)
        {
            //Debug.Log("�|�[�Y��Enter");
            GameManager.Instance.IsPause(true,"�|�[�Y��");
        }
        protected override void OnUpdate()
        {
            //Debug.Log("�|�[�Y��Update");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                stateMachine.Dispatch((int)StateEvent.Resume);
            }
        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("�|�[�Y��Exit");
            GameManager.Instance.IsPause(false,"");
        }
    }

    class LevelUpState : State
    {
        protected override void OnEnter(State prevState)
        {
            //Debug.Log("���x���A�b�vEnter");
            GameManager.Instance.IsPause(true, "���x���I��");
            GameManager.Instance.SkillButton.SelectStart();
        }
        protected override void OnUpdate()
        {
            //Debug.Log("���x���A�b�vUpdate");
        }

        protected override void OnExit(State nextState)
        {
            GameManager.Instance.IsPause(false, "���x���I��");
            //Debug.Log("���x���A�b�vExit");
        }
    }

    class InGameState : State
    {
        protected override void OnEnter(State prevState)
        {
            //Debug.Log("�Q�[����Enter");
        }
        protected override void OnUpdate()
        {
            //Debug.Log("�Q�[����Update");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                stateMachine.Dispatch((int)StateEvent.Pause);
            }
        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("�Q�[����Exit");
        }
    }

    class ResultState : State
    {
        protected override void OnEnter(State prevState)
        {
            //Debug.Log("���U���gEnter");
            GameManager.Instance.IsPause(true, "�Q�[���I�[�o�[");
        }

        protected override void OnUpdate()
        {
            //Debug.Log("���U���gUpdate");
        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("���U���gExit");
            GameManager.Instance.Reset();
        }
    }


}
/// <summary>
/// �J�ڂ����邽�߂̃C�x���g
/// </summary>
public enum StateEvent : int
{
    GameStart,
    Pause,
    Resume,
    LevelUp,
    GameOver,
    ReStart,
}
