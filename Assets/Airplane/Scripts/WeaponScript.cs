using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Airplane
{
    public class WeaponScript : MonoBehaviour
    {
        [SerializeField] private Transform _shotPrefab;
        [SerializeField] private float _shootingRate = 0.25f;
        private float _shootCooldown = 0f;

        private void Update()
        {
            if (_shootCooldown > 0)
            {
                _shootCooldown -= Time.deltaTime;
            }
        }

        public void Attack(bool isEnemy)
        {
            if (CanAttack)
            {
                _shootCooldown = _shootingRate;

                var shotTransform = Instantiate(_shotPrefab);

                shotTransform.position = transform.position;

                if (shotTransform.TryGetComponent(out ShotScript shot))
                {
                    shot.IsEnemyShot = isEnemy;
                }

                if (shotTransform.TryGetComponent(out MoveScript move))
                {
                    move.Direction = transform.right; // в двухмерном пространстве это будет справа от спрайта
                }
            }
        }

        public bool CanAttack
        {
            get
            {
                return _shootCooldown <= 0;
            }
        }
    }
}