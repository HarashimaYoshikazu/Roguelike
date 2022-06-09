using UniRx;
using UnityEngine;
using State = StateMachine<GameCycle>.State;

public class GameCycle : MonoBehaviour
{
    StateMachine<GameCycle> _stateMachine = null;

    /// <summary>
    /// �J�ڂ����邽�߂̃C�x���g
    /// </summary>
    enum StateEvent:int
    {
        GameStart,
        GameOver,
        ReStart,
    }
    private void Awake()
    {
        //�������R���X�g���N�^�͂��̃N���X��ۑ�
        _stateMachine = new StateMachine<GameCycle>(this);

        //�J�ڂ�o�^
        _stateMachine.AddTransition<StartState, InGameState>((int)StateEvent.GameStart);
        _stateMachine.AddTransition<InGameState, ResultState>((int)StateEvent.GameOver);
        _stateMachine.AddTransition<ResultState, StartState>((int)StateEvent.ReStart);

        //�ŏ���State��ݒ�AEnte�֐����Ăяo��
        _stateMachine.StartSetUp<StartState>();
    }

    //�{�^���f�o�b�O�p
    //������
    public void GoIngame()
    {
        _stateMachine.Dispatch((int)StateEvent.GameStart);
    }
    public void GoResult()
    {
        _stateMachine.Dispatch((int)StateEvent.GameOver);
    }

    public void GoStart()
    {
        _stateMachine.Dispatch((int)StateEvent.ReStart);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    class StartState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log("�͂��܂�Enter");
        }

        //protected override void OnUpdate()
        //{
        //    Debug.Log("�͂��܂�Update");
        //}

        protected override void OnExit(State nextState)
        {
            Debug.Log("�͂��܂�Exit");
        }
    }

    class InGameState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log("�Q�[����Enter");
        }
        //protected override void OnUpdate()
        //{
        //    Debug.Log("�Q�[����Update");
        //}

        protected override void OnExit(State nextState)
        {
            Debug.Log("�Q�[����Exit");
        }
    }

    class ResultState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log("���U���gEnter");
        }

        //protected override void OnUpdate()
        //{
        //    Debug.Log("���U���gUpdate");
        //}

        protected override void OnExit(State nextState)
        {
            Debug.Log("���U���gExit");
        }
    }


}
