using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float _launchSpeed = 3;
        [SerializeField] private float _maxSpeed = 50;
        [SerializeField] private float _pushForce = 50;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {

        }

        private void FixedUpdate()
        {
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _maxSpeed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Movement>())
            {
                return;
            }
            _rb.AddForce(collision.contacts[0].normal * _pushForce);
        }

        public void Launch()
        {
            float x = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
            float y = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
            _rb.velocity = new Vector2(x, y) * _launchSpeed;
        }
    }
}
