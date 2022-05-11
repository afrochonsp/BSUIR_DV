using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Airplane
{
    public class ScrollingScript : MonoBehaviour
    {
        // Скорость прокрутки
        [SerializeField] private float _speed = 1;

        // Направление движения
        [SerializeField] private Vector2 _direction = new Vector2(-1, 0);

        // Движения должны быть применены к камере
        [SerializeField] private bool _isLinkedToCamera = false;

        // 1 – Бесконечный фон
        [SerializeField] private bool _isLooping = false;

        // 2 – Список детей с рендерером
        private List<Transform> _backgroundPart;

        public void StopIfLinked()
        {
            if(_isLinkedToCamera)
            {
                _speed = 0;
            }
        }

        void Start()
        {
            if (_isLooping)
            {
                // Задействовать всех детей слоя с рендерером
                _backgroundPart = new List<Transform>();

                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);

                    // Добавить только видимых детей
                    if (child.GetComponent<Renderer>() != null)
                    {
                        _backgroundPart.Add(child);
                    }
                }

                // Сортировка по позиции.
                // Примечание: получаем детей слева направо.
                // Мы должны добавить несколько условий для обработки
                // разных направлений прокрутки.
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
                    // Проверить, находится ли ребенок (частично) перед камерой.
                    if (firstChild.position.x < Camera.main.transform.position.x)
                    {
                        // Если ребенок уже слева от камеры,
                        // мы проверяем, покинул ли он область кадра, чтобы использовать его
                        // повторно.
                        if (firstChild.GetComponent<Renderer>().isVisible == false)
                        {
                            // Получить последнюю позицию ребенка.
                            Transform lastChild = _backgroundPart.LastOrDefault();
                            Vector3 lastPosition = lastChild.transform.position;
                            Vector3 lastSize = (lastChild.GetComponent<Renderer>().bounds.max - lastChild.GetComponent<Renderer>().bounds.min);

                            // Переместить повторно используемый объект так, чтобы он располагался ПОСЛЕ
                            // последнего ребенка
                            // Примечание: Пока работает только для горизонтального скроллинга.
                            firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);

                            // Поставить повторно используемый объект
                            // в конец списка backgroundPart.
                            _backgroundPart.Remove(firstChild);
                            _backgroundPart.Add(firstChild);
                        }
                    }
                }
            }
        }
    }
}