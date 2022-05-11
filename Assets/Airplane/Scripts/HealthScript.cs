using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Airplane
{
    public class HealthScript : MonoBehaviour
    {
        [SerializeField] private int _hp = 1;
        [SerializeField] private bool _isEnemy = true;
        public int HP { get { return _hp; } set { _hp = value; } }

        public void Damage(int damageCount)
        {
            _hp -= damageCount;

            if (_hp <= 0)
            {
                SpecialEffectsHelper.Instance.Explosion(transform.position);
                SoundEffectsHelper.Instance.MakeExplosionSound();
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.TryGetComponent(out ShotScript shot))
            {
                if (shot.IsEnemyShot != _isEnemy)
                {
                    Damage(shot.Damage);
                    Destroy(shot.gameObject);
                }
            }
        }
    }
}