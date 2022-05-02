using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PingPong
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Goal : MonoBehaviour
    {
        [SerializeField] PlayersEnum _player;
        [SerializeField] UnityEvent<Collider2D, PlayersEnum> _onTriggerEnter;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            _onTriggerEnter.Invoke(collider, _player);
        }

        private void Reset()
        {
            GetComponent<Collider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}