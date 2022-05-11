using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour
    {
        [SerializeField] private float _flipSpeed = 180;
        [SerializeField] private GameObject _back;

        public int ID { get; private set; }

        public void SetCard(int id, Sprite image)
        {
            ID = id;
            GetComponent<SpriteRenderer>().sprite = image;
        }

        public void OnMouseDown()
        {
            if (_back.activeSelf && SceneController.instance.CanReveal)
            {
                SceneController.instance.CardRevealed(this);
                StopAllCoroutines();
                StartCoroutine(Flip(180));
            }
        }

        public void Unreveal()
        {
            StopAllCoroutines();
            StartCoroutine(Flip(0));
        }

        private float _velocity = 0.0f;

        private IEnumerator Flip(float angle)
        {
            transform.localEulerAngles = new Vector3(0, Mathf.SmoothDampAngle(transform.localEulerAngles.y, angle, ref _velocity, _flipSpeed), 0);
            if (angle >= 180 && transform.localEulerAngles.y > 90)
            {
                _back.SetActive(false);
            }
            else if(angle < 180 && transform.localEulerAngles.y < 90)
            {
                _back.SetActive(true);
            }
            yield return null;
            if (Mathf.Abs(transform.localEulerAngles.y - angle) > 1)
            {
                StartCoroutine(Flip(angle));
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}

