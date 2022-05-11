using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Airplane
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveScript : MonoBehaviour
    {
        [SerializeField] private float _speed = 10;
        [SerializeField] private Vector2 _direction = new Vector2(-1, 0);
        private Vector2 _movement;
        private Rigidbody2D _rb;
        public Vector2 Direction { get { return _direction; } set { _direction = value; } }

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _movement = new Vector2(_direction.x, _direction.y) * _speed;
        }

        private void FixedUpdate()
        {
            _rb.velocity = _movement;
        }
    }
}

