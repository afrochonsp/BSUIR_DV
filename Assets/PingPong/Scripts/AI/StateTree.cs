using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEditor;

[Serializable]
public class NextState
{
    public StateTree stateTree;
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
            Condition condition = CreateInstance<Condition>(conditionMono.GetClass());
            condition._stateMachine = stateMachine;
            Conditions.Add(condition);
        }
        stateTree.Init(stateMachine);
    }

    public void EnterConditions()
    {
        foreach (var condition in Conditions)
        {
            condition.EnterCondition();
        }
    }

    T CreateInstance<T>(Type type, params object[] paramArray)
    {
        return (T)Activator.CreateInstance(type, args: paramArray);
    }
}

[CreateAssetMenu(fileName = "New State Tree", menuName = "State Tree", order = 51)]
public class StateTree : ScriptableObject
{
    [Tooltip("Namespace.Type")]
    [SerializeField] private MonoScript _state;
    public State State { get; private set; }
    public List<NextState> _nextStates;

    public State GetStateFromString(string typeName, StateMachine stateMachine)
    {
        Type type = Type.GetType(typeName);
        State state = (State)Activator.CreateInstance(type, stateMachine);
        return state;
    }

    public void Init(StateMachine stateMachine)
    {
        State = CreateInstance<State>(_state.GetClass());
        State._stateMachine = stateMachine;
        foreach (var nextState in _nextStates)
        {
            nextState.Init(stateMachine);
        }
    }

    T CreateInstance<T>(Type type, params object[] paramArray)
    {
        return (T)Activator.CreateInstance(type, args: paramArray);
    }
}