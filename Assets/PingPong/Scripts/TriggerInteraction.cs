using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

namespace PingPong
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerInteraction : MonoBehaviour
    {
        [SerializeField] MonoScript _type;
        public UnityEvent _enteredTrigger;
        public UnityEvent _exitedTrigger;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent(_type.GetClass()))
            {
                _enteredTrigger.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent(_type.GetClass()))
            {
                _exitedTrigger.Invoke();
            }
        }
    }
}

