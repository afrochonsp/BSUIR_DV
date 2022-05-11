using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Airplane
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] private Vector2 _speed = new Vector2(10, 10);
        private Vector2 _movement;
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");


            _movement = new Vector2(
              _speed.x * inputX,
              _speed.y * inputY);

            bool shoot = Input.GetButtonDown("Fire1");
            shoot |= Input.GetButtonDown("Fire2");

            if (shoot && TryGetComponent(out WeaponScript weapon))
            {
                weapon.Attack(false);
            }

            // 6 Ц ”бедитьс€, что игрок не выходит за рамки кадра
            var dist = (transform.position - Camera.main.transform.position).z;

            var leftBorder = Camera.main.ViewportToWorldPoint(
              new Vector3(0, 0, dist)
            ).x;

            var rightBorder = Camera.main.ViewportToWorldPoint(
              new Vector3(1, 0, dist)
            ).x;

            var topBorder = Camera.main.ViewportToWorldPoint(
              new Vector3(0, 0, dist)
            ).y;

            var bottomBorder = Camera.main.ViewportToWorldPoint(
              new Vector3(0, 1, dist)
            ).y;

            transform.position = new Vector3(
              Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
              Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
              transform.position.z
            );
        }

        private void FixedUpdate()
        {
            _rb.velocity = _movement;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            bool damagePlayer = false;

            if (collision.gameObject.TryGetComponent(out EnemyScript enemy))
            {
                if (collision.gameObject.TryGetComponent(out HealthScript enemyHealth))
                {
                    enemyHealth.Damage(enemyHealth.HP);
                }
                damagePlayer = true;
            }

            if (damagePlayer && TryGetComponent(out HealthScript playerHealth))
            {
                playerHealth.Damage(1);
            }
        }
    }
}