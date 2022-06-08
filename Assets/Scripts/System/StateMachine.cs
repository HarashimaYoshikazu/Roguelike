using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StateMachine<TOwner>
{
    //ステート本体
    public ReactiveProperty<State> CurrentState { get; set; } = new ReactiveProperty<State>();

    /// <summary>
    /// このステートマシンのオーナー
    /// </summary>
    public TOwner Owner { get; }

    //実行ブリッジ
    //public void Execute() => CurrentState.Value.

    public StateMachine(TOwner owner)
    {
        Owner = owner;
    }

    public abstract class State
    {
        public StateMachine<TOwner> stateMachine;
        /// <summary>
        /// 遷移の一覧
        /// </summary>
        internal Dictionary<int, State> transitions = new Dictionary<int, State>();

        /// <summary>
        /// ステート開始
        /// </summary>
        public void Enter(State prevState)
        {
            OnEnter(prevState);
        }
        /// <summary>
        /// ステートを開始した時に呼ばれる
        /// </summary>
        public virtual void OnEnter(State prevState) { }

        /// <summary>
        /// ステート更新
        /// </summary>
        public void Update()
        {
            OnUpdate();
        }
        /// <summary>
        /// 毎フレーム呼ばれる
        /// </summary>
        public virtual void OnUpdate() { }

        /// <summary>
        /// ステート終了
        /// </summary>
        public void Exit()
        {
            OnExit();
        }
        /// <summary>
        /// ステートを終了した時に呼ばれる
        /// </summary>
        public virtual void OnExit() { }

        public abstract State GetState();
    }

    // ステートリスト
    private LinkedList<State> states = new LinkedList<State>();

    /// <summary>
    /// ステートを追加する（ジェネリック版）
    /// </summary>
    public T Add<T>() where T : State, new()
    {
        var state = new T();
        state.stateMachine = this;
        states.AddLast(state);
        return state;
    }

    /// <summary>
    /// 特定のステートを取得、なければ生成する
    /// </summary>
    public T GetOrAddState<T>() where T : State, new()
    {
        foreach (var state in states)
        {
            if (state is T result)
            {
                return result;
            }
        }
        return Add<T>();
    }

    public void AddTransition<TFrom, TTo>(int eventId)
        where TFrom : State, new()
        where TTo : State, new()
    {
        var from = GetOrAddState<TFrom>();
        if (from.transitions.ContainsKey(eventId))
        {
            // 同じイベントIDの遷移を定義済
            throw new System.ArgumentException(
                $"ステート'{nameof(TFrom)}'に対してイベントID'{eventId.ToString()}'の遷移は定義済です");
        }

        var to = GetOrAddState<TTo>();
        from.transitions.Add(eventId, to);
    }

    public void ChangeState(State nextstate)
    {
        CurrentState.Value.Exit();
        nextstate.Enter(CurrentState.Value);
        CurrentState.Value = nextstate;
    }
}
