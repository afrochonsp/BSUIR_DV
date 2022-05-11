using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Airplane
{
    public class ShotScript : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private bool _isEnemyShot = false;
        public int Damage { get { return _damage; } private set { _damage = value; } }
        public bool IsEnemyShot { get { return _isEnemyShot; } set { _isEnemyShot = value; } }

        private void Start()
        {
            Destroy(gameObject, 20);
        }
    }
}