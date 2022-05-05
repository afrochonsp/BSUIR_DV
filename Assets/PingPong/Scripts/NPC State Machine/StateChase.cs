using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong
{
    public class StateChase : State
    {

        public override void EnterState()
        {
            ((NPC_StateMachine)_stateMachine).NPC.MoveType = MoveTypeEnum.Hard;
        }

        public override void UpdateState()
        {
            ((NPC_StateMachine)_stateMachine).NPC.TargetPosition = ((NPC_StateMachine)_stateMachine).Ball.gameObject.transform.position;
        }
    }
}