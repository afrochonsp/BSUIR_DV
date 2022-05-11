using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Airplane
{
    public class EnemyScript : MonoBehaviour
    {
        private bool _hasSpawn = false;
        private MoveScript _moveScript;
        private WeaponScript[] _weapons;

        void Awake()
        {
            _weapons = GetComponentsInChildren<WeaponScript>();
            _moveScript = GetComponent<MoveScript>();
        }

        void Start()
        {
            GetComponent<Collider2D>().enabled = false;
            _moveScript.enabled = false;
            foreach (WeaponScript weapon in _weapons)
            {
                weapon.enabled = false;
            }
        }

        void Update()
        {
            // 2 - Проверить, начался ли спавн врагов.
            if (_hasSpawn == false)
            {
                if (GetComponent<Renderer>().isVisible)
                {
                    Spawn();
                }
            }
            else
            {
                // автоматическая стрельба
                foreach (WeaponScript weapon in _weapons)
                {
                    if (weapon != null && weapon.enabled && weapon.CanAttack)
                    {
                        weapon.Attack(true);
                        SoundEffectsHelper.Instance.MakeEnemyShotSound();
                    }
                }

                if (!GetComponent<Renderer>().isVisible)
                {
                    Destroy(gameObject);
                }
            }
        }

        // 3 - Самоактивация.
        private void Spawn()
        {
            _hasSpawn = true;
            GetComponent<Collider2D>().enabled = true;
            _moveScript.enabled = true;
            foreach (WeaponScript weapon in _weapons)
            {
                weapon.enabled = true;
            }
        }
    }
}
