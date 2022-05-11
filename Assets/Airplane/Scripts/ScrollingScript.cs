using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Airplane
{
    public class ScrollingScript : MonoBehaviour
    {
        [SerializeField] private float _speed = 1;
        [SerializeField] private Vector2 _direction = new Vector2(-1, 0);
        [SerializeField] private bool _isLinkedToCamera = false;
        [SerializeField] private bool _isLooping = false;
        private List<Transform> _backgroundPart;

        public void StopIfLinked()
        {
            if(_isLinkedToCamera)
            {
                _speed = 0;
            }
        }

        private void Start()
        {
            if (_isLooping)
            {
                _backgroundPart = new List<Transform>();

                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);
                    if (child.GetComponent<Renderer>() != null)
                    {
                        _backgroundPart.Add(child);
                    }
                }

                _backgroundPart = _backgroundPart.OrderBy(
                  t => t.position.x
                ).ToList();
            }
        }

        private void Update()
        {
            Vector3 movement = new Vector3(_direction.x, _direction.y, 0) * _speed;

            movement *= Time.deltaTime;
            transform.Translate(movement);

            if (_isLinkedToCamera)
            {
                Camera.main.transform.Translate(movement);
            }

            if (_isLooping)
            {
                Transform firstChild = _backgroundPart.FirstOrDefault();

                if (firstChild != null)
                {
                    if (firstChild.position.x < Camera.main.transform.position.x)
                    {
                        if (firstChild.GetComponent<Renderer>().isVisible == false)
                        {
                            Transform lastChild = _backgroundPart.LastOrDefault();
                            Vector3 lastPosition = lastChild.transform.position;
                            Vector3 lastSize = (lastChild.GetComponent<Renderer>().bounds.max - lastChild.GetComponent<Renderer>().bounds.min);
                            firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);
                            _backgroundPart.Remove(firstChild);
                            _backgroundPart.Add(firstChild);
                        }
                    }
                }
            }
        }
    }
}