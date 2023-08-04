using System;
using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
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
        [SerializeField] private ScoreData scoreData;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject highScoreIndicator;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button exitButton;
        [SerializeField] [NaughtyAttributes.Scene] private string mainMenuScene;

        private void Awake()
        {
            panel.SetActive(false);
            retryButton.onClick.AddListener(Retry);
            exitButton.onClick.AddListener(Exit);
        }

        private void OnEnable()
        {
            GameManager.OnShadowCollide.AddListener(ShowResultScreen);
        }

        private void OnDisable()
        {
            GameManager.OnShadowCollide.RemoveListener(ShowResultScreen);
        }
        
        private void Retry()
        {
            var _name = gameObject.scene.name;
            SceneManager.LoadScene(_name, LoadSceneMode.Single);
            Debug.Log($"Reload scene: {_name}");
        }
        
        private void Exit()
        {
            SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Single);
        }

        private void ShowResultScreen()
        {
            StartCoroutine(Co_ResultScreen());
        }

        private IEnumerator Co_ResultScreen()
        {
            Time.timeScale = 0f;
            EnableButtons(false);
            var _score = ScoreManager.Instance.score;
            var _res = scoreData.AddScore(_score);
            scoreData.SaveHighScore();
            highScoreIndicator.SetActive(false);
            
            scoreText.text = "0";
            panel.SetActive(true);

            yield return new WaitForSecondsRealtime(0.1f);
            
            yield return DOTween
                .To(() => 0, x => scoreText.text = $"{x}", _score, 1f)
                .SetEase(Ease.Linear).SetUpdate(true).WaitForCompletion();

            yield return new WaitForSecondsRealtime(0.5f);

            highScoreIndicator.SetActive(_res is {Item1: true, Item2: 1});

            yield return new WaitForSecondsRealtime(1.0f);
            
            EnableButtons(true);
        }

        private async void ResulScreenAsync()
        {
            Time.timeScale = 0f;
            EnableButtons(false);
            var _score = ScoreManager.Instance.score;
            var _res = scoreData.AddScore(_score);
            scoreData.SaveHighScore();
            highScoreIndicator.SetActive(false);
            
            scoreText.text = "Score: 0";
            panel.SetActive(true);

            await Task.Delay(10);

            await DOTween
                .To(() => 0, x => scoreText.text = $"Score: {x}", _score, 1f)
                .SetEase(Ease.Linear).SetUpdate(true).AsyncWaitForCompletion();

            await Task.Delay(10);

            highScoreIndicator.SetActive(_res is {Item1: true, Item2: 1});
            
            await Task.Delay(500);
            
            EnableButtons(true);
        }
        
        private void EnableButtons(bool enable_)
        {
            retryButton.gameObject.SetActive(enable_);
            exitButton.gameObject.SetActive(enable_);
        }
    }
}
