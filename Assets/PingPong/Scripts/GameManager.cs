using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PingPong
{
    public class GameManager : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private PlayerMovement _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;

        [Header("NPC")]
        [SerializeField] private NPC_Movement _NPC_Prefab;
        [SerializeField] private Transform _NPC_SpawnPoint;
        [SerializeField] private Transform _NPC_PatrolZone;

        [Header("Ball")]
        [SerializeField] private Ball _ballPrefab;
        [SerializeField] private Transform _ballSpawnPoint;
        [SerializeField] private Transform _ballSpawnPointLeft;
        [SerializeField] private Transform _ballSpawnPointRight;

        [Header("UI")]
        [SerializeField] private Text _score;

        private NPC_Movement _NPC;
        private PlayerMovement _player;
        private Ball _ball;
        private int scoreLeft;
        private int scoreRight;

        private void Start()
        {
            _ball = Instantiate(_ballPrefab, _ballSpawnPoint.position, _ballSpawnPoint.rotation);
            _player = Instantiate(_playerPrefab, _playerSpawnPoint.position, _playerSpawnPoint.rotation);
            _NPC = Instantiate(_NPC_Prefab, _NPC_SpawnPoint.position, _NPC_SpawnPoint.rotation);
            _NPC.PatrolZone = _NPC_PatrolZone;
            _NPC.GetComponent<NPC_StateMachine>().Ball = _ball;
        }

        public void Goal(Collider2D collider, PlayersEnum player)
        {
            if (!collider.GetComponent<Ball>())
            {
                return;
            }
            Respawn(player);
        }

        private void Respawn(PlayersEnum player)
        {
            _NPC.transform.position = _NPC_SpawnPoint.position;
            _player.transform.position = _playerSpawnPoint.position;
            _ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if(scoreLeft > 3 || scoreRight > 3)
            {
                scoreLeft = scoreRight = 0;
                _ball.transform.position = _ballSpawnPoint.position;
            }
            else if (player == PlayersEnum.RightPlayer)
            {
                scoreLeft++;
                _ball.transform.position = _ballSpawnPointRight.position;
            }
            else
            {
                _ball.transform.position = _ballSpawnPointLeft.position;
                scoreRight++;
            }
            _score.text = scoreLeft.ToString() + "/" + scoreRight.ToString();
        }

        public void OnBallEnteredAttackZone()
        {
            _NPC.GetComponent<NPC_StateMachine>().InAttackZone = true;
        }

        public void OnBallExitedAttackZone()
        {
            _NPC.GetComponent<NPC_StateMachine>().InAttackZone = false;
        }
    }
}