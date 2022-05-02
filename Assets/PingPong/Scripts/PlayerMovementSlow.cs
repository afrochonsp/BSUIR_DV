using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementSlow : Movement
    {
        [SerializeField] private float _speed = 1;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _rb.MovePosition(new Vector2(transform.position.x + Input.GetAxis("Mouse X") * _speed, transform.position.y + Input.GetAxis("Mouse Y") * _speed));
        }
    }
}