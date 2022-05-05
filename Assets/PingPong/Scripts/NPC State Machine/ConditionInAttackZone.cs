using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong
{
    public class ConditionInAttackZone : Condition
    {
        public override bool Check()
        {
            if (((NPC_StateMachine)_stateMachine).InAttackZone)
            {
                return true;
            }
            return false;
        }
    }
}
