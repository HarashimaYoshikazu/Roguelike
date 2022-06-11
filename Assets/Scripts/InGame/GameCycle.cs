using UniRx;
using UnityEngine;
using System;
using State = StateMachine<GameCycle>.State;

public class GameCycle : MonoBehaviour
{
    StateMachine<GameCycle> _stateMachine = null;

    /// <summary>
    /// 遷移をするためのイベント
    /// </summary>
    enum StateEvent:int
    {
        GameStart,
        Pause,
        Resume,
        GameOver,
        ReStart,
    }
    private void Awake()
    {
        //初期化コンストラクタはこのクラスを保存
        _stateMachine = new StateMachine<GameCycle>(this);

        //遷移を登録
        _stateMachine.AddTransition<StartState, InGameState>((int)StateEvent.GameStart);
        _stateMachine.AddTransition<InGameState, ResultState>((int)StateEvent.GameOver);
        _stateMachine.AddTransition<InGameState,PauseState>((int)StateEvent.Pause);
        _stateMachine.AddTransition<PauseState, InGameState>((int)StateEvent.Resume);
        _stateMachine.AddTransition<ResultState, StartState>((int)StateEvent.ReStart);

        //最初のStateを設定、Ente関数を呼び出し
        _stateMachine.StartSetUp<StartState>();
    }

    //ボタンデバッグ用
    //※消す
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
            Debug.Log("はじまりEnter");
            GameManager.Instance.IsPause(true);
        }

        protected override void OnUpdate()
        {
            Debug.Log("はじまりUpdate");
        }

        protected override void OnExit(State nextState)
        {
            Debug.Log("はじまりExit");
            GameManager.Instance.IsPause(false);
        }
    }

    class PauseState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log("ポーズ中Enter");
            GameManager.Instance.IsPause(true);
        }
        protected override void OnUpdate()
        {
            Debug.Log("ポーズ中Update");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                stateMachine.Dispatch((int)StateEvent.Resume);
            }
        }

        protected override void OnExit(State nextState)
        {
            Debug.Log("ポーズ中Exit");
            GameManager.Instance.IsPause(false);
        }
    }


    class InGameState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log("ゲーム中Enter");
        }
        protected override void OnUpdate()
        {
            Debug.Log("ゲーム中Update");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                stateMachine.Dispatch((int)StateEvent.Pause);
            }
        }

        protected override void OnExit(State nextState)
        {
            Debug.Log("ゲーム中Exit");
        }
    }

    class ResultState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log("リザルトEnter");
        }

        protected override void OnUpdate()
        {
            Debug.Log("リザルトUpdate");
        }

        protected override void OnExit(State nextState)
        {
            Debug.Log("リザルトExit");
        }
    }


}
