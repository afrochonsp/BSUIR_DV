using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong
{
    public enum MoveTypeEnum { Smooth, Hard };

    [RequireComponent(typeof(Rigidbody2D))]
    public class NPC_Movement : Movement
    {
        [SerializeField] private float _speed = 1;
        [SerializeField] private float _speedHardMylty = 3;
        private Rigidbody2D _rb;

        public MoveTypeEnum MoveType { get; set; } = MoveTypeEnum.Smooth;

        public Transform PatrolZone { get; set; }
        public Vector2 TargetPosition { get; set; }
        private Vector2 _currentVelocity;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (MoveType == MoveTypeEnum.Smooth)
            {
                _rb.MovePosition(Vector2.SmoothDamp(transform.position, TargetPosition, ref _currentVelocity, 1 / _speed));
            }
            else
            {
                _rb.MovePosition(Vector2.MoveTowards(transform.position, TargetPosition, _speed * _speedHardMylty));
            }
        }

        public void MoveToRandomPoint()
        {
            float x = Random.Range(PatrolZone.position.x - PatrolZone.localScale.x / 2, PatrolZone.position.x + PatrolZone.localScale.x / 2);
            float y = Random.Range(PatrolZone.position.y - PatrolZone.localScale.y / 2, PatrolZone.position.y + PatrolZone.localScale.y / 2);
            TargetPosition = new Vector2(x, y);
        }
    }
}