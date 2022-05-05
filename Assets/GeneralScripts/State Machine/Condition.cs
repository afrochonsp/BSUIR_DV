using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition
{
    public StateMachine _stateMachine;
    public virtual void EnterCondition() { }
    public abstract bool Check();
}
