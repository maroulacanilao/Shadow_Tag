using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.ResultScreen
{
    public class ResultScreenUI : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            panel.SetActive(false);
            retryButton.onClick.AddListener(Retry);
            exitButton.onClick.AddListener(Retry);
        }
        
        private void OnEnable()
        {
            GameManager.OnShadowCollide.AddListener(ShowResultScreen);
        }

        private void OnDisable()
        {
            GameManager.OnShadowCollide.RemoveListener(ShowResultScreen);
        }

        private void ShowResultScreen()
        {
            Time.timeScale = 0f;
            scoreText.text = $"Score: {GameManager.Instance.score}";
            
            panel.SetActive(true);
        }
        
        private void Retry()
        {
            var _name = gameObject.scene.name;
            SceneManager.LoadScene(_name, LoadSceneMode.Single);
        }
    }
}
