using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour
    {
        [SerializeField] private GameObject _back;
        private SceneController _controller;

        public int ID { get; private set; }

        private SpriteRenderer _sprite;

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _controller = SceneController.instance;
        }

        public void SetCard(int id, Sprite image)
        {
            ID = id;
            _sprite.sprite = image;
        }

        public void OnMouseDown()
        {
            if (_back.activeSelf && _controller.CanReveal)
            {
                _back.SetActive(false);
                _controller.CardRevealed(this);
            }
        }

        public void Unreveal()
        {
            _back.SetActive(true);
        }
    }
}

