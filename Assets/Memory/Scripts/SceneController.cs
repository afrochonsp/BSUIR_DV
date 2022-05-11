using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Memory
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private Card _originalCard;
        [SerializeField] private Sprite[] _images;
        [SerializeField] private TextMesh _scoreLabel;

        public static SceneController instance;

        private const int _gridRows = 2;
        private const int _gridCols = 4;
        private const float _offsetX = 2f;
        private const float _offsetY = 2.5f;

        private Card _firstRevealed;
        private Card _secondRevealed;
        private int _score = 0;

        public bool CanReveal => _secondRevealed == null;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void CardRevealed(Card card)
        {
            if (_firstRevealed == null)
            {
                _firstRevealed = card;
            }
            else
            {
                _secondRevealed = card;
                StartCoroutine(CheckMatch());
            }
        }

        private IEnumerator CheckMatch()
        {
            if (_firstRevealed.ID == _secondRevealed.ID)
            {
                _score++;
                _scoreLabel.text = "Score: " + _score;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                _firstRevealed.Unreveal();
                _secondRevealed.Unreveal();
            }
            _firstRevealed = null;
            _secondRevealed = null;
        }

        private void Start()
        {
            Vector3 startPos = _originalCard.transform.position;
            List<int> numbers = new List<int>(new int[_gridCols * _gridRows]);
            int tempNumber = 0;
            for (int i = 0; i < numbers.Count; i += 2)
            {
                numbers[i] = tempNumber;
                if(i >= numbers.Count + 1)
                {
                    break;
                }
                numbers[i + 1] = tempNumber;
                tempNumber++;
            }
            ShuffleList(numbers);
            for (int i = 0; i < _gridCols; i++)
            {
                for (int j = 0; j < _gridRows; j++)
                {
                    Card card;
                    if (i == 0 && j == 0)
                    {
                        card = _originalCard;
                    }
                    else
                    {
                        card = Instantiate(_originalCard);
                    }
                    int index = j * _gridCols + i;
                    int id = numbers[index];
                    card.SetCard(id, _images[id]);
                    float posX = (_offsetX * i) + startPos.x;
                    float posY = -(_offsetY * j) + startPos.y;
                    card.transform.position = new Vector3(posX, posY, startPos.z);
                }
            }
        }

        private void ShuffleList(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int tmp = list[i];
                int r = Random.Range(i, list.Count);
                list[i] = list[r];
                list[r] = tmp;
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
