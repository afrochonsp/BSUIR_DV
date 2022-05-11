using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEditor;

[Serializable]
public class NextState
{
    public StateObject stateObject;
    [SerializeField] public List<MonoScript> _conditions;
    public List<Condition> Conditions { get; private set; } = new List<Condition>();

    [NonSerialized]
    bool inited = false;

    public void Init(StateMachine stateMachine)
    {
        if (inited)
        {
            return;
        }
        inited = true;
        foreach (var conditionMono in _conditions)
        {
            Condition condition = (Condition)Activator.CreateInstance(conditionMono.GetClass());
            condition._stateMachine = stateMachine;
            Conditions.Add(condition);
        }
        stateObject.Init(stateMachine);
    }

    public void EnterConditions()
    {
        foreach (var condition in Conditions)
        {
            condition.EnterCondition();
        }
    }
}

[CreateAssetMenu(fileName = "New State Object", menuName = "State Object", order = 51)]
public class StateObject : ScriptableObject
{
    [Tooltip("Namespace.Type")]
    [SerializeField] private MonoScript _state;
    public State State { get; private set; }
    public List<NextState> _nextStates;

    public void Init(StateMachine stateMachine)
    {
        State = (State)Activator.CreateInstance(_state.GetClass());
        State._stateMachine = stateMachine;
        foreach (var nextState in _nextStates)
        {
            nextState.Init(stateMachine);
        }
    }
}