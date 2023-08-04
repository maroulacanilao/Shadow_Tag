using System;
using Managers;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Pause
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject highScorePanel;
        [SerializeField] [Scene] private string mainMenuScene;

        bool isPaused = false;
        
        private void Awake()
        {
            InputManager.OnPauseAction.AddListener(OnPauseAction);
        }

        private void OnDestroy()
        {
            InputManager.OnPauseAction.RemoveListener(OnPauseAction);
        }
        
        private void OnEnable()
        {
            highScorePanel.SetActive(false);
            pausePanel.SetActive(false);
            isPaused = false;
        }

        private void OnPauseAction(InputAction.CallbackContext context)
        {
            if(!context.started) return;
            TogglePause();
        }

        public void Pause()
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            highScorePanel.SetActive(false);
            isPaused = true;
        }
        
        public void UnPause()
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            isPaused = false;
        }
        
        public void TogglePause()
        {
            isPaused = !isPaused;
            
            if (isPaused) Pause();
            else UnPause();

        }
        
        public void Restart()
        {
            UnPause();
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        
        public void MainMenu()
        {
            UnPause();
            UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuScene);
        }
        
        public void Quit()
        {
            Application.Quit();
        }
    }
}
