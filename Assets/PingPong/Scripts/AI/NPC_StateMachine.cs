using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PingPong
{
    public class NPC_StateMachine : StateMachine
    {
        public NPC_Movement NPC { get; private set; }
        public Ball Ball { get; set; }

        public bool InAttackZone { get; set; }

        protected override void Init()
        {
            NPC = GetComponent<NPC_Movement>();
        }
    }
}

