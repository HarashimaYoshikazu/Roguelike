using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StateMachine<TOwner>
{
    //�X�e�[�g�{��
    public ReactiveProperty<State> CurrentState { get; set; } = new ReactiveProperty<State>();

    /// <summary>
    /// ���̃X�e�[�g�}�V���̃I�[�i�[
    /// </summary>
    public TOwner Owner { get; }

    //���s�u���b�W
    //public void Execute() => CurrentState.Value.

    public StateMachine(TOwner owner)
    {
        Owner = owner;
    }

    public abstract class State
    {
        public StateMachine<TOwner> stateMachine;
        /// <summary>
        /// �J�ڂ̈ꗗ
        /// </summary>
        internal Dictionary<int, State> transitions = new Dictionary<int, State>();

        /// <summary>
        /// �X�e�[�g�J�n
        /// </summary>
        public void Enter(State prevState)
        {
            OnEnter(prevState);
        }
        /// <summary>
        /// �X�e�[�g���J�n�������ɌĂ΂��
        /// </summary>
        public virtual void OnEnter(State prevState) { }

        /// <summary>
        /// �X�e�[�g�X�V
        /// </summary>
        public void Update()
        {
            OnUpdate();
        }
        /// <summary>
        /// ���t���[���Ă΂��
        /// </summary>
        public virtual void OnUpdate() { }

        /// <summary>
        /// �X�e�[�g�I��
        /// </summary>
        public void Exit()
        {
            OnExit();
        }
        /// <summary>
        /// �X�e�[�g���I���������ɌĂ΂��
        /// </summary>
        public virtual void OnExit() { }

        public abstract State GetState();
    }

    // �X�e�[�g���X�g
    private LinkedList<State> states = new LinkedList<State>();

    /// <summary>
    /// �X�e�[�g��ǉ�����i�W�F�l���b�N�Łj
    /// </summary>
    public T Add<T>() where T : State, new()
    {
        var state = new T();
        state.stateMachine = this;
        states.AddLast(state);
        return state;
    }

    /// <summary>
    /// ����̃X�e�[�g���擾�A�Ȃ���ΐ�������
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
            // �����C�x���gID�̑J�ڂ��`��
            throw new System.ArgumentException(
                $"�X�e�[�g'{nameof(TFrom)}'�ɑ΂��ăC�x���gID'{eventId.ToString()}'�̑J�ڂ͒�`�ςł�");
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
