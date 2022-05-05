using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong
{
    public class StatePatrol : State
    {
        private readonly float _delay = 1;
        private float _lastInvokeTime;
        private float _currentTime;

        public override void EnterState()
        {
            _lastInvokeTime = Time.time;
            ((NPC_StateMachine)_stateMachine).NPC.MoveType = MoveTypeEnum.Smooth;
            Patrol();
        }

        public override void UpdateState()
        {
            _currentTime = Time.time;
            if (_currentTime - _lastInvokeTime > _delay)
            {
                Patrol();
                _lastInvokeTime = _currentTime;
            }
        }

        public void Patrol()
        {
            ((NPC_StateMachine)_stateMachine).NPC.MoveToRandomPoint();
        }
    }
}