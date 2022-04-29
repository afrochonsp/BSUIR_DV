using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float _launchSpeed = 3;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Launch();
        }

        // Update is called once per frame
        private void Update()
        {

        }

        public void Launch()
        {
            float x = Random.Range(0, 2) == 0 ? -1 : 1;
            float y = Random.Range(0, 2) == 0 ? -1 : 1;
            _rb.velocity = new Vector2(x, y) * _launchSpeed;
        }
    }
}
