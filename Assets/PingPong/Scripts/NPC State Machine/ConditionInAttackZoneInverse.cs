using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong
{
    public class ConditionInAttackZoneInverse : Condition
    {
        public override bool Check()
        {
            if (((NPC_StateMachine)_stateMachine).InAttackZone)
            {
                return false;
            }
            return true;
        }
    }
}
