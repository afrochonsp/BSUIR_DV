using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong
{
    public class ConditionTime : Condition
    {

        [SerializeField] float _time = 5;
        private float _enterTime;

        public override void EnterCondition()
        {
            base.EnterCondition();
            _enterTime = Time.time;
        }

        public override bool Check()
        {
            if (Time.time - _enterTime > _time)
            {
                return true;
            }
            return false;
        }
    }
}

