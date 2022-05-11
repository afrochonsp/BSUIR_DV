using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Airplane
{
    public class BossScript : MonoBehaviour
    {
        private bool hasSpawn;

        // Параметры компонентов
        private MoveScript _moveScript;
        private WeaponScript[] _weapons;
        private Animator _animator;
        private SpriteRenderer[] _renderers;

        // Поведение босса (не совсем AI)
        [SerializeField] private float _minAttackCooldown = 0.5f;
        [SerializeField] private float _maxAttackCooldown = 2f;

        private float _aiCooldown;
        private bool _isAttacking;
        private Vector2 _positionTarget;

        void Awake()
        {
            _weapons = GetComponentsInChildren<WeaponScript>();
            _moveScript = GetComponent<MoveScript>();
            _animator = GetComponent<Animator>();
            _renderers = GetComponentsInChildren<SpriteRenderer>();
        }

        void Start()
        {
            hasSpawn = false;
            GetComponent<Collider2D>().enabled = false;
            _moveScript.enabled = false;
            foreach (WeaponScript weapon in _weapons)
            {
                weapon.enabled = false;
            }
            _isAttacking = false;
            _aiCooldown = _maxAttackCooldown;
        }

        void Update()
        {
            if (hasSpawn == false)
            {
                // Для простоты проверим только первый рендерер
                // Но мы не знаем, если это тело, и глаз или рот ...
                if (_renderers[0].isVisible)
                {
                    Spawn();
                }
            }
            else
            {
                _aiCooldown -= Time.deltaTime;

                if (_aiCooldown <= 0f)
                {
                    _isAttacking = !_isAttacking;
                    _aiCooldown = Random.Range(_minAttackCooldown, _maxAttackCooldown);
                    _positionTarget = Vector2.zero;
                    _animator.SetBool("Attack", _isAttacking);
                }
                if (_isAttacking)
                {
                    _moveScript.Direction = Vector2.zero;

                    foreach (WeaponScript weapon in _weapons)
                    {
                        if (weapon != null && weapon.enabled && weapon.CanAttack)
                        {
                            weapon.Attack(true);
                            SoundEffectsHelper.Instance.MakeEnemyShotSound();
                        }
                    }
                }
                else
                {
                    // Выбрать цель?
                    if (_positionTarget == Vector2.zero)
                    {
                        // Получить точку на экране, преобразовать ее в цель в игровом мире
                        Vector2 randomPoint = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

                        _positionTarget = Camera.main.ViewportToWorldPoint(randomPoint);
                    }

                    // У нас есть цель? Если да, найти новую
                    if (GetComponent<Collider2D>().OverlapPoint(_positionTarget))
                    {
                        // Сбросить, выбрать в следующем кадре
                        _positionTarget = Vector2.zero;
                    }

                    // Идти к точке
                    Vector3 direction = ((Vector3)_positionTarget - transform.position);

                    // Помните об использовании скрипта движения
                    _moveScript.Direction = Vector3.Normalize(direction);
                }
            }
        }

        private void Spawn()
        {
            hasSpawn = true;
            GetComponent<Collider2D>().enabled = true;
            _moveScript.enabled = true;
            foreach (WeaponScript weapon in _weapons)
            {
                weapon.enabled = true;
            }

            // Остановить основной скроллинг
            foreach (ScrollingScript scrolling in FindObjectsOfType<ScrollingScript>())
            {
                scrolling.StopIfLinked();
            }
        }

        void OnTriggerEnter2D(Collider2D otherCollider2D)
        {
            // В случае попадания изменить анимацию
            if (otherCollider2D.TryGetComponent(out ShotScript shot))
            {
                if (shot.IsEnemyShot == false)
                {
                    _aiCooldown = Random.Range(_minAttackCooldown, _maxAttackCooldown);
                    _isAttacking = false;
                    _animator.SetTrigger("Hit");
                    Invoke("ResetHit", 0.2f);
                }
            }
        }

        void OnDrawGizmos()
        {
            // Можно отобразить отладочную информацию в вашей сцене с Гизмо
            if (hasSpawn && _isAttacking == false)
            {
                Gizmos.DrawSphere(_positionTarget, 0.25f);
            }
        }

        private void ResetHit()
        {
            _animator.ResetTrigger("Hit");
        }
    }
}

