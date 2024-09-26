using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<TState> where TState : State
{
    public TState currentState { get; private set; }

    public void Initialize(TState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(TState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
