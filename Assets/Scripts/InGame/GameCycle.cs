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
        //初期化コンストラクタはこのクラスを保存
        _stateMachine = new StateMachine<GameCycle>(this);

        //遷移を登録
        _stateMachine.AddTransition<StartState, InGameState>((int)StateEvent.GameStart);
        _stateMachine.AddTransition<InGameState, ResultState>((int)StateEvent.GameOver);
        _stateMachine.AddTransition<InGameState, LevelUpState>((int)StateEvent.LevelUp);
        _stateMachine.AddTransition<LevelUpState, InGameState>((int)StateEvent.GameStart);
        _stateMachine.AddTransition<InGameState, PauseState>((int)StateEvent.Pause);
        _stateMachine.AddTransition<PauseState, InGameState>((int)StateEvent.Resume);
        _stateMachine.AddTransition<ResultState, StartState>((int)StateEvent.ReStart);

        //最初のStateを設定、Ente関数を呼び出し
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
            //Debug.Log("はじまりEnter");            
            GameManager.Instance.IsPause(true,"ボタンで開始");
            
        }

        protected override void OnUpdate()
        {
            //Debug.Log("はじまりUpdate");
        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("はじまりExit");
            GameManager.Instance.IsPause(false,"");
        }
    }

    class PauseState : State
    {
        protected override void OnEnter(State prevState)
        {
            //Debug.Log("ポーズ中Enter");
            GameManager.Instance.IsPause(true,"ポーズ中");
        }
        protected override void OnUpdate()
        {
            //Debug.Log("ポーズ中Update");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                stateMachine.Dispatch((int)StateEvent.Resume);
            }
        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("ポーズ中Exit");
            GameManager.Instance.IsPause(false,"");
        }
    }

    class LevelUpState : State
    {
        protected override void OnEnter(State prevState)
        {
            //Debug.Log("レベルアップEnter");
            GameManager.Instance.IsPause(true, "レベル選択");
            GameManager.Instance.SkillButton.SelectStart();
        }
        protected override void OnUpdate()
        {
            //Debug.Log("レベルアップUpdate");
        }

        protected override void OnExit(State nextState)
        {
            GameManager.Instance.IsPause(false, "レベル選択");
            //Debug.Log("レベルアップExit");
        }
    }

    class InGameState : State
    {
        protected override void OnEnter(State prevState)
        {
            //Debug.Log("ゲーム中Enter");
        }
        protected override void OnUpdate()
        {
            //Debug.Log("ゲーム中Update");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                stateMachine.Dispatch((int)StateEvent.Pause);
            }
        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("ゲーム中Exit");
        }
    }

    class ResultState : State
    {
        protected override void OnEnter(State prevState)
        {
            //Debug.Log("リザルトEnter");
            GameManager.Instance.IsPause(true, "ゲームオーバー");
        }

        protected override void OnUpdate()
        {
            //Debug.Log("リザルトUpdate");
        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("リザルトExit");
            GameManager.Instance.Reset();
        }
    }


}
/// <summary>
/// 遷移をするためのイベント
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
