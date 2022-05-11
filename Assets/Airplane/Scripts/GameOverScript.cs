using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Airplane
{
    public class GameOverScript : MonoBehaviour
    {
        [SerializeField] RectTransform _UI;
        public static GameOverScript Instance;

        private void Awake()
        {
            if(Instance)
            {
                Debug.LogError("Несколько экземпляров SoundEffectsHelper!");
                Destroy(this);
            }
            Instance = this;
        }

        public void ShowUI()
        {
            if(_UI)
            {
                _UI.gameObject.SetActive(true);
            }
        }

        public void HideUI()
        {
            if(_UI)
            {
                _UI.gameObject.SetActive(false);
            }
        }


        public void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
