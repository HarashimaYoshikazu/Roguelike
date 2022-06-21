using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<TestCycle>.State;

public class TestCycle :MonoBehaviour
{
    StateMachine<TestCycle> _statemachine = null;

    public enum StateChangeEvent
    {
        GameStart,
        GameOver,
        Retry
    }

    void Start()
    {
        _statemachine = new StateMachine<TestCycle>(this);

        //�J�ڂ��`
        _statemachine.AddTransition<StartState,InGameState>((int)StateChangeEvent.GameStart);
        _statemachine.AddTransition<InGameState,ResultState>((int)StateChangeEvent.GameOver);
        _statemachine.AddTransition<ResultState,StartState>((int)StateChangeEvent.Retry);

        _statemachine.StartSetUp<StartState>();

    }
    [SerializeField]
    public UnityEngine.UI.Text _text = null;
    
    public void GameStart()
    {
        _statemachine.Dispatch((int)StateChangeEvent.GameStart);
    }

    public void GameOver()
    {
        _statemachine.Dispatch((int)StateChangeEvent.GameOver);
    }

    public void Retry()
    {
        _statemachine.Dispatch((int)StateChangeEvent.Retry);
    }

    void Update()
    {
        //_statemachine.Update();
    }


    //State�̐錾
    class StartState : State
    {
        protected override void OnEnter(State prevState)
        {
            Owner._text.text = "�X�^�[�g���";
        }
    }

    class InGameState : State
    {
        protected override void OnEnter(State prevState)
        {
            Owner._text.text = "�Q�[�����イ";
        }
    }

    class ResultState:State
    {
        protected override void OnEnter(State prevState)
        {
            Owner._text.text = "���U���g���";
        }
    }


}
