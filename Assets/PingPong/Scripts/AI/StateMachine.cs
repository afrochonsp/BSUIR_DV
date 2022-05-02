using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public abstract class StateMachine : MonoBehaviour
{
    [SerializeField] StateTree StartState;
    public State CurrentState { get; private set; }
    public List<NextState> NextStates { get; private set; }

    protected virtual void Init() { }

    private void Start()
    {
        Init();
        StartState.Init(this);
        CurrentState = StartState.State;
        NextStates = StartState._nextStates;
        foreach (var _nextState in NextStates)
        {
            _nextState.EnterConditions();
        }
        CurrentState.EnterState();
    }

    private void Update()
    {
        CurrentState.UpdateState();
        CheckTransitions();
    }

    private void CheckTransitions()
    {
        foreach(var nextState in NextStates)
        {
            foreach (var condition in nextState.Conditions)
            {
                if (condition.Check())
                {
                    ChangeState(nextState.stateTree.State);
                    NextStates = nextState.stateTree._nextStates;
                    foreach (var _nextState in NextStates)
                    {
                        _nextState.EnterConditions();
                    }
                    return;
                }
            }
        }
    }

    public void ChangeState(State state)
    {
        CurrentState = state;
        state.EnterState();
    }
}
