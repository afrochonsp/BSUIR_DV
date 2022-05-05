using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public StateMachine _stateMachine;
    public abstract void EnterState();
    public abstract void UpdateState();
}