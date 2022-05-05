using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class UIButton : MonoBehaviour
    {
        [SerializeField] private GameObject _targetObject;
        [SerializeField] private string _targetMethod = "Restart";
        [SerializeField] private Color _highlightedColor = Color.cyan;
        [SerializeField] private Vector3 _pressedScale = new Vector3(2.2f, 2.2f, 2.2f);

        private SpriteRenderer _sprite;
        private Vector3 _defaultScale;
        private Color _defaultColor;

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _defaultScale = transform.localScale;
            _defaultColor = _sprite.color;
        }

        public void OnMouseOver()
        {
            _sprite.color = _highlightedColor;
        }

        public void OnMouseExit()
        {
            _sprite.color = _defaultColor;
        }

        public void OnMouseDown()
        {
            transform.localScale = _pressedScale;
        }

        public void OnMouseUp()
        {
            transform.localScale = _defaultScale;
            _targetObject?.SendMessage(_targetMethod);
        }
    }
}
