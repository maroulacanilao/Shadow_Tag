using Managers;
using TMPro;
using UnityEngine;

namespace UI.HUD
{
    public class GameHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        
        private void OnEnable()
        {
            GameManager.OnUpdateScore.AddListener(UpdateScore);
        }
        
        private void OnDisable()
        {
            GameManager.OnUpdateScore.RemoveListener(UpdateScore);
        }
        
        private void UpdateScore(int score_)
        {
            scoreText.text = $"Score: {score_}";
        }
    }
}