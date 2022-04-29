using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 1;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, Input.GetAxis("Vertical") * _speed);
        }
    }
}

